using SQ.DecisionTreeWorkflow.Services.Models;
using SQ.Senior.Quoting.Internal.DatabaseAdapter.Models;
using System.Threading.Tasks;

namespace SQ.Senior.SpecialEnrollmentPeriods.Handlers {
    public interface IQuestionHandler {
        Task<string> GetApplicantMappedValueAsync(AgentVersionApplicant applicant, Question question);
    }
}
