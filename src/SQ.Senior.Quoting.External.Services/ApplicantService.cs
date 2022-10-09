using SQ.Senior.Clients.QrsService.Infrastructure.Constants;
using SQ.Senior.Clients.QrsService.Infrastructure.IQRSServices;
using SQ.Senior.Clients.QrsService.Models;
using SQ.Senior.Quoting.DatabaseAdapter.Models;
using SQ.Senior.Quoting.External.Enums;
using SQ.Senior.Quoting.External.Services.Helpers;
using SQ.Senior.Quoting.External.Services.IServices;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SQ.Senior.Quoting.External.Services {
    public class ApplicantService : BaseService, IApplicantService {
        private readonly IQrsService _qrsService;
        private readonly IUserService _userService;
        private ApplicantTracking _applicantTracking;
        private readonly IZipFipsMappingService _zipFipsMappingService;
        private QRSApplicantsTokens applicantToken;
        private ApplicantTracking _applicantTrackingZipFip;

        public ApplicantService(IQrsService qrsService, IZipFipsMappingService zipFipsMappingService, IUserService userService) : base(userService) {
            _qrsService = qrsService;
            _zipFipsMappingService = zipFipsMappingService;
            _applicantTracking = new ApplicantTracking();
            _userService = userService;
            applicantToken = new QRSApplicantsTokens();
            _applicantTrackingZipFip = new ApplicantTracking();
        }

        public async Task<QRSApplicantsTokens> GetApplicantByTokenAsync(string token, string userId = "")
            => await _qrsService.GetApplicantByTokenAsync(Endpoints.ApplicantToken, token, userId);

        public async Task<QRSApplicants> GetApplicantByApplicantIdAsync(string applicantId, string userId)
            => await _qrsService.GetApplicantByApplicantIdAsync(Endpoints.Applicants, applicantId, userId);

        public async Task<QRSApplicants> GetConsolidatedDataByApplicantIdAsync(string applicantId, string userId = null)
            => await _qrsService.GetConsolidatedDataByApplicantIdAsync(Endpoints.ApplicantConsolidate, applicantId, userId);

        public async Task<ApplicantTracking> GetApplicantZipFipsByApplicantIdAsync(string applicantId, string zipCode, string userId = null) {
            var applicantConsolidated = await GetConsolidatedDataByApplicantIdAsync(applicantId, userId);
            if (applicantConsolidated is null)
                return _applicantTracking;

            var consolidatedContacts = applicantConsolidated.Contacts
                .GroupBy(u => u.ContactTypeId).Select(group => group.OrderByDescending(d => d.DateCreated).FirstOrDefault()).ToList();
            var applicantZipFips = consolidatedContacts.FirstOrDefault(c => c.ContactTypeId == (int)ContactTypes.Zipcode);

            if (applicantZipFips == null)
                return _applicantTracking;

            _applicantTracking.ZIPCode = applicantZipFips.ZipCode;
            _applicantTracking.FipsCode = applicantZipFips.FipsCode;

            if (!string.IsNullOrEmpty(zipCode)) {
                var fipsCodes = await _zipFipsMappingService.GetFipsCode(zipCode);
                if (fipsCodes != null && fipsCodes.Count == 1) {
                    _applicantTracking.FipsCode = fipsCodes.First().CountyFipsCode;
                }
            }

            return _applicantTracking;
        }
        public async Task<long> GetCurrentInteractionIdAsync(string userId, BrandType brandType)
                => await _userService.ManageUserInteractionSessionAsync(brandType, userId);
        public async Task<ApplicantTracking> SaveApplicantTrackingPurlAsync(PurlParams purlParams, string userId, BrandType brandType, string purlType) {

            var applicantRequest = new QRSApplicants { DateCreated = DateTime.UtcNow };
            var qrsApplicantResponse = await _qrsService.AddApplicantsAsync(applicantRequest, brandType, userId);
            long? qrsApplicantId = 0;
            long? interactionId = 0;

            if (purlType.Equals(PurlType.Marketing)) {
                if (!string.IsNullOrWhiteSpace(purlParams.Token)) {
                    applicantToken = await GetApplicantByTokenAsync(purlParams.Token, userId);
                    if (applicantToken != null) {
                        qrsApplicantId = _qrsService.AddInteraction(ConvertHelper.GetIntValue(applicantToken.ApplicantId), brandType, userId);
                        interactionId = Convert.ToInt64(applicantToken.ApplicantId);
                        _applicantTrackingZipFip = await GetApplicantZipFipsByApplicantIdAsync(applicantToken.ApplicantId, purlParams.ZipCode, userId);
                        _applicantTracking.ZIPCode = _applicantTrackingZipFip?.ZIPCode;
                        _applicantTracking.FipsCode = _applicantTrackingZipFip?.FipsCode;
                    }
                }
            } else {
                qrsApplicantId = qrsApplicantResponse?.ApplicantId ?? 0;
                interactionId = qrsApplicantResponse is null ? 0
                    : _qrsService.AddInteraction(qrsApplicantResponse.ApplicantId, brandType, userId);
            }

            _applicantTracking = ApplicantTrackingPurlMapping(purlParams, userId, purlType, qrsApplicantId, interactionId);
            _applicantTracking = await _userService.SaveApplicantTrackingAsync(_applicantTracking);
            return _applicantTracking;
        }

    }

}

