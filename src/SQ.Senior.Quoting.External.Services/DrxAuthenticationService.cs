using SQ.Senior.Clients.DrxServices.Infrastructure.Services;
using SQ.Senior.Clients.DrxServices.Models;
using SQ.Senior.Quoting.External.Services.Helpers;
using SQ.Senior.Quoting.External.Services.Infrastructure.Constants;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SQ.Senior.Quoting.External.Services.IServices {
    public class DrxAuthenticationService : IDrxAuthenticationService {

        private readonly IPrescriptionService _prescriptionService;
        private readonly IUserService _userService;
        private readonly IUserPlanService _userPlanService;
        BaseService _baseService;
        private ApplicantCompleteInfo _applicantCompleteInfo;
        private readonly IDrxApiClient _drxApiClient;
        public DrxAuthenticationService(IPrescriptionService prescriptionService, IUserService userService, IUserPlanService userPlanService, IDrxApiClient drxApiClient) {
            _prescriptionService = prescriptionService;
            _userService = userService;
            _userPlanService = userPlanService;
            _baseService = new BaseService(_userService);
            _applicantCompleteInfo = new ApplicantCompleteInfo();
            _drxApiClient = drxApiClient;
        }
        //TODO: Use this method in the method of QuoteDataForDrxPlanAsync in userplanservice.
        public async Task<string> GetDrxAuthentication(string userId) {
            if (string.IsNullOrWhiteSpace(userId)) {
                return null;
            }

            var xmlSessionData = string.Empty;
            var selectedPrescriptions = new List<SelectedPrescriptions>();
            
            var prescriptions = await _prescriptionService.GetPrescriptionAsync(userId);
            prescriptions.ForEach(s => { selectedPrescriptions.Add(_baseService.DrxPresciptionMapping(s, null)); });
           
            var applicant = await _userService.GetUserAsync(userId);
            var applicantTracking = await _userService.GetApplicantTrackingAsync(userId);            
            var applicantCompleteInfo = await _userPlanService.GetApplicantInfoAsync(userId, applicantTracking?.EntryToken);


            SetExtraHelpValue(ConvertHelper.GetIntValue(applicant?.ExtraHelpLevel ?? 0));
            var drxToken = await _drxApiclient.GetDrxTokenAsync();
            var drxSession = await _drxApiClient.GetDrxSessionAsync(selectedPrescriptions, applicantCompleteInfo, Status.HealthStatus, drxToken, setDrxXml => xmlSessionData = setDrxXml);
            return null;
        }
        private void SetExtraHelpValue(int extraHelpValue) {
            switch (ConvertHelper.GetStringValue(extraHelpValue)) {
                case "1":
                    _applicantCompleteInfo.SubsidyLevel = 4;
                    break;

                case "2":
                    _applicantCompleteInfo.SubsidyLevel = 1;
                    break;

                case "3":
                    _applicantCompleteInfo.SubsidyLevel = 5;
                    break;

                case "4":
                    _applicantCompleteInfo.SubsidyLevel = 2;
                    break;
                case "5":
                    _applicantCompleteInfo.SubsidyLevel = 3;
                    _applicantCompleteInfo.SubsidyPercent = 0;
                    break;
                //ExtraHelpLevel_5a is the default value if we select ExtraHelpLevel_5 or ExtraHelpLevel_5a
                // Any one selectin both of these 
                case "0": // 
                    _applicantCompleteInfo.SubsidyLevel = 3;
                    _applicantCompleteInfo.SubsidyPercent = 0;
                    break;
                case "25":
                    _applicantCompleteInfo.SubsidyLevel = 3;
                    _applicantCompleteInfo.SubsidyPercent = 25;
                    break;
                case "50":
                    _applicantCompleteInfo.SubsidyLevel = 3;
                    _applicantCompleteInfo.SubsidyPercent = 50;
                    break;
                case "75":
                    _applicantCompleteInfo.SubsidyLevel = 3;
                    _applicantCompleteInfo.SubsidyPercent = 75;
                    break;
                case "100":
                    _applicantCompleteInfo.SubsidyLevel = 3;
                    _applicantCompleteInfo.SubsidyPercent = 100;
                    break;
                case "6":
                    _applicantCompleteInfo.SubsidyLevel = 4;
                    break;
                default:
                    _applicantCompleteInfo.SubsidyLevel = null;
                    break;
            }
        }
    }
}
