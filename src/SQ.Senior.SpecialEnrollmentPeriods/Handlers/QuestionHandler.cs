using SQ.DecisionTreeWorkflow.DatabaseAdapter.Models;
using SQ.DecisionTreeWorkflow.DatabaseAdapter.Repositories;
using SQ.Senior.Quoting.Internal.DatabaseAdapter.Models;
using SQ.Senior.SpecialEnrollmentPeriods.Constants;
using System;
using System.Linq;
using System.Threading.Tasks;
using Question = SQ.DecisionTreeWorkflow.Services.Models.Question;
using SQ.Senior.SpecialEnrollmentPeriods.Marx.Services;
using SQ.Senior.SpecialEnrollmentPeriods.Marx.Models;
using SQ.Senior.SpecialEnrollmentPeriods.Marx.Enums;
using SQ.Senior.SpecialEnrollmentPeriods.Extensions;

namespace SQ.Senior.SpecialEnrollmentPeriods.Handlers {
    public class QuestionHandler : IQuestionHandler {
        private readonly IDecisionTreeWorkflowDbRepository _workflowDbRepository;
        private readonly IMarxService _marxService;

        public QuestionHandler(IDecisionTreeWorkflowDbRepository workflowDbRepository, IMarxService marxService) {
            _workflowDbRepository = workflowDbRepository ?? throw new ArgumentNullException(nameof(workflowDbRepository));
            _marxService = marxService ?? throw new ArgumentNullException(nameof(marxService));
        }

        public async Task<string> GetApplicantMappedValueAsync(AgentVersionApplicant applicant, Question question) {
            var previousAnswer = await GetPreviousAnswer(applicant, question.Id);
            if (previousAnswer != null) {
                if (previousAnswer.DateValue.HasValue) {
                    return previousAnswer.DateValue.Value.ToString("MM/dd/yyyy");
                }

                if (previousAnswer.BooleanValue.HasValue) {
                    return previousAnswer.BooleanValue.Value ? "1" : "0";
                }
            }

            if (string.IsNullOrWhiteSpace(question?.MetaDataTag)) {
                return default(string);
            }

            var marxResponse = GetMarxResponse(applicant?.MBI);

            var (medicareEntitlementPartADate, medicareEntitlementPartBDate) = marxResponse is null ? (null, null) :
                                                                                GetMedicareEntitlementDates(marxResponse);

            applicant.PartAEffectiveDate = medicareEntitlementPartADate ?? applicant.PartAEffectiveDate;
            applicant.PartBEffectiveDate = medicareEntitlementPartBDate ?? applicant.PartBEffectiveDate;

            return GetMappedValueByMetaDataTag(question.MetaDataTag, applicant);
        }

        private async Task<QuestionAnswer> GetPreviousAnswer(AgentVersionApplicant applicant, int questionId) {
            var answers = await _workflowDbRepository.FindMultipeByAsync<QuestionAnswer>(q => q.QuestionId == questionId && 
                q.ApplicantId == applicant.SQSApplicantId && 
                q.AccountId == applicant.SQSAccountId);

            return answers?.OrderByDescending(a => a.Id).FirstOrDefault();
        }

        private static string GetMappedValueByMetaDataTag(string metaDataTag, AgentVersionApplicant applicant) {
            switch (metaDataTag) {
                case MetadataTag.DateOfBirth:
                    return GetDateString(applicant?.BirthDate);
                case MetadataTag.MedicarePartAEffectiveDate:
                    return GetDateString(applicant?.PartAEffectiveDate);
                case MetadataTag.MedicarePartBEffectiveDate:
                    return GetDateString(applicant?.PartBEffectiveDate);
                case MetadataTag.CurrentDate:
                    return GetDateString(DateTime.Now);
                default:
                    return default(string);
            }
        }

        private MarxSearchCasesResponse GetMarxResponse(string mbi) {
            return string.IsNullOrWhiteSpace(mbi) ? null :
                 _marxService.SearchCases(new MarxSearchCasesRequest() { IdentifierId = mbi, IdentifierType = IdentifierType.MBI }).Result;
        }

        private static (DateTime?, DateTime?) GetMedicareEntitlementDates(MarxSearchCasesResponse marxResponse) {

            Func<MedicareEntitlementInformation, string, string, bool> partFinder =
                (MedicareEntitlementInformation info, string part, string option) => info.Part == part && info.Option.In(option, "Y");

            var medicareEntitlementPartA = marxResponse?.Capture?.MedicareEntitlementInformation?.Find(info => partFinder(info, "A", "E"));
            var medicareEntitlementPartB = marxResponse?.Capture?.MedicareEntitlementInformation?.Find(info => partFinder(info, "A", "G"));

            return (medicareEntitlementPartA?.StartDate, medicareEntitlementPartB?.StartDate);
        }

        private static string GetDateString(DateTime? date) {
            if (!date.HasValue) {
                return default(string);
            }

            return date.Value.ToString("MM/dd/yyyy");
        }
    }
}
