using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SQ.Senior.Clients.DrxServices.Infrastructure.Services;
using SQ.Senior.Clients.DrxServices.Models;
using SQ.Senior.Clients.QrsService.Infrastructure.Constants;
using SQ.Senior.Clients.QrsService.Infrastructure.IQRSServices;
using SQ.Senior.Clients.QrsService.Models;
using SQ.Senior.Quoting.DatabaseAdapter.Abstractions;
using SQ.Senior.Quoting.DatabaseAdapter.Models;
using SQ.Senior.Quoting.External.Services.Helpers;
using SQ.Senior.Quoting.External.Services.Infrastructure.Constants;
using SQ.Senior.Quoting.External.Services.IServices;
using SQ.Senior.Quoting.External.Services.ViewModels.Pharmacy;
using SQ.Senior.Quoting.External.Services.ViewModels.Plan;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SQ.Senior.Quoting.External.Services {
    public class UserPlanService : BaseService, IUserPlanService {
        private readonly ISQSeniorExternalQuotingDbContext _dbContext;
        private readonly IQrsService _qrsService;
        private readonly IPharmacyService _pharmacyService;
        private readonly IApplicantService _applicantService;
        private readonly IAppLogService _appLogService;
        private readonly IDrxApiClient _drxApiClient;
        private readonly IAuthentication _authentication;
        private readonly IPrescriptionService _prescriptionService;
        private long? _qrsApplicantId;
        private ApplicantCompleteInfo applicantCompleteInfo;

        public UserPlanService(ISQSeniorExternalQuotingDbContext dbContext, IQrsService qrsService, IPharmacyService pharmacyService, IApplicantService applicantService, IAppLogService appLogService,
            IUserService userService, IAuthentication authentication, IDrxApiClient drxApiClient, IPrescriptionService prescriptionService) : base(userService) {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _qrsService = qrsService ?? throw new ArgumentNullException(nameof(qrsService));
            _drxApiClient = drxApiClient;
            _prescriptionService = prescriptionService;
            _authentication = authentication;
            _pharmacyService = pharmacyService;
            _appLogService = appLogService;
            _applicantService = applicantService;
            applicantCompleteInfo = new ApplicantCompleteInfo();
        }

        #region Plan
        public async Task<Plans> GetDrxPlansAsync(GetPlansRequest getPlansRequest) {

            if (getPlansRequest is null || string.IsNullOrWhiteSpace(getPlansRequest.UserId) || string.IsNullOrWhiteSpace(getPlansRequest.ZipCode)
            || string.IsNullOrWhiteSpace(getPlansRequest.FipsCode))
                return null;

            var sessionInfoDrxApi = await QuoteDataForDrxPlanAsync(getPlansRequest);
            var plansXml = await _drxApiClient.GetPlans(
                                            sessionInfoDrxApi.DrxSessionId,
                                            sessionInfoDrxApi.ZipCode, sessionInfoDrxApi.FipsCode,
                                            sessionInfoDrxApi.ViewPlanYear,
                                            sessionInfoDrxApi.DrxAccessToken);
            if (string.IsNullOrWhiteSpace(plansXml)) {
                return null;
            }
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(plansXml))) {
                return (Plans)new XmlSerializer(typeof(Plans)).Deserialize(stream);
            }
        }
        private async Task<SessionInfoDrxApi> QuoteDataForDrxPlanAsync(GetPlansRequest getPlansRequest) {

            if (getPlansRequest is null || string.IsNullOrWhiteSpace(getPlansRequest.UserId) || string.IsNullOrWhiteSpace(getPlansRequest.ZipCode)
            || string.IsNullOrWhiteSpace(getPlansRequest.FipsCode))
                return null;

            var zipCode = ConvertHelper.GetStringValue(getPlansRequest.ZipCode);
            var fipsCode = ConvertHelper.GetStringValue(getPlansRequest.FipsCode);
            var userId = ConvertHelper.GetStringValue(getPlansRequest.UserId);
            var xmlSessionData = string.Empty;

            var selectedPrescriptions = new List<SelectedPrescriptions>();
            var healthStatus = Status.HealthStatus;
            var prescriptions = await _prescriptionService.GetPrescriptionAsync(userId);
            if (prescriptions != null) {
                foreach (var prescription in prescriptions) {
                    if (prescription.Id > 0) {
                        var prescriptionInfo = PlanHelpers.GetPrescriptionInfo(prescription.SelectedPrescriptionInfoAsXml);
                        selectedPrescriptions.Add(DrxPresciptionMapping(prescription, prescriptionInfo));
                    }
                }
            }

            applicantCompleteInfo = await GetApplicantInfoAsync(userId, ConvertHelper.GetStringValue(getPlansRequest.PurlToken ?? string.Empty));
            SetExtraHelpValue(ConvertHelper.GetStringValue(getPlansRequest.ExtraHelpLevel));
            var drxToken = await _authentication.GetDrxTokenAsync();
            var drxSession = await _authentication.GetDrxSessionAsync(selectedPrescriptions, applicantCompleteInfo, healthStatus, drxToken, setDrxXml => xmlSessionData = setDrxXml);
            var pharmacy = await _pharmacyService.GetPharmacyAsync(userId);
            var drxPharmacyData = await SetDrxPharmacySession(userId, getPlansRequest.PharmacyType, pharmacy, drxToken?.AccessToken ?? string.Empty, drxSession?.SessionId ?? string.Empty);

            if (!string.IsNullOrWhiteSpace(drxPharmacyData.Item1)) {
                await _appLogService.InsertAppLogAsync(
                typeof(UserPlanService).Name,
                Status.Success,
                zipCode,
                "QuoteDataForDrxPlanAsync",
                string.Empty,
                ConvertHelper.GetStringValue(fipsCode),
                applicantCompleteInfo?.FirstName ?? Status.Anonymous,
                drxPharmacyData.Item2,
                userId,
                drxPharmacyData.Item1,
                ConvertHelper.GetStringValue(drxSession.SessionId));
            }

            return new SessionInfoDrxApi {
                DrxSessionId = drxSession.SessionId,
                DrxAccessToken = drxToken.AccessToken,
                ZipCode = zipCode,
                FipsCode = fipsCode,
                ViewPlanYear = getPlansRequest.PlanYear
            };
        }
        private async Task<Tuple<string, string>> SetDrxPharmacySession(string userId, string pharmacyType, PharmacyResponse pharmacy, string drxToken, string drxSessionId) {

            if (pharmacyType.Equals(PharmacyType.RetailPharmacy) && pharmacy != null) {
                var pharmacyDrxRecord = new PharmacyDrxRecord {
                    PharmacyPhone = pharmacy.PharmacyPhone,
                    PharmacyID = pharmacy.PharmacyDrxId,
                    PharmacyRecordID = 1,
                };
                var pharmacyRecordAPIJson = $"[{JsonConvert.SerializeObject(pharmacyDrxRecord)}]";
                return Tuple.Create(pharmacyRecordAPIJson, await _drxApiClient.AddSessionPharmacy(drxSessionId, drxToken, pharmacyRecordAPIJson));
            }
            var deletedPharmacyJson = await _drxApiClient.DeleteSessionPharmacy(drxSessionId, drxToken);
            await _pharmacyService.DeletePharmacyAsync(userId);
            return Tuple.Create(Status.Deleted, deletedPharmacyJson);
        }
        private void SetExtraHelpValue(string extraHelpValue) {
            switch (ConvertHelper.GetStringValue(extraHelpValue)) {
                case "1":
                    applicantCompleteInfo.SubsidyLevel = 4;
                    break;

                case "2":
                    applicantCompleteInfo.SubsidyLevel = 1;
                    break;

                case "3":
                    applicantCompleteInfo.SubsidyLevel = 5;
                    break;

                case "4":
                    applicantCompleteInfo.SubsidyLevel = 2;
                    break;
                case "5":
                    applicantCompleteInfo.SubsidyLevel = 3;
                    applicantCompleteInfo.SubsidyPercent = 0;
                    break;
                //ExtraHelpLevel_5a is the default value if we select ExtraHelpLevel_5 or ExtraHelpLevel_5a
                // Any one selectin both of these 
                case "0": // 
                    applicantCompleteInfo.SubsidyLevel = 3;
                    applicantCompleteInfo.SubsidyPercent = 0;
                    break;
                case "25":
                    applicantCompleteInfo.SubsidyLevel = 3;
                    applicantCompleteInfo.SubsidyPercent = 25;
                    break;
                case "50":
                    applicantCompleteInfo.SubsidyLevel = 3;
                    applicantCompleteInfo.SubsidyPercent = 50;
                    break;
                case "75":
                    applicantCompleteInfo.SubsidyLevel = 3;
                    applicantCompleteInfo.SubsidyPercent = 75;
                    break;
                case "100":
                    applicantCompleteInfo.SubsidyLevel = 3;
                    applicantCompleteInfo.SubsidyPercent = 100;
                    break;
                case "6":
                    applicantCompleteInfo.SubsidyLevel = 4;
                    break;
                default:
                    applicantCompleteInfo.SubsidyLevel = null;
                    break;
            }
        }
        public async Task<ApplicantCompleteInfo> GetApplicantInfoAsync(string userId, string purlToken) {

            var applicantProfile = await GetApplicantProfileAsync(userId);
            if (applicantProfile != null) {
                applicantCompleteInfo.FirstName = applicantProfile.FirstName ?? "null";
                applicantCompleteInfo.LastName = applicantProfile.LastName ?? "null";
                if (applicantProfile.DateOfBirth != null) {
                    applicantProfile.DateOfBirth = applicantProfile.DateOfBirth != DateTime.MinValue ? applicantProfile.DateOfBirth : null; // check applied on this null , remove node.
                }
                applicantCompleteInfo.EmailAddress = applicantProfile.Email ?? "null";
                applicantCompleteInfo.Gender = "null";
            } else if (!String.IsNullOrWhiteSpace(purlToken)) {
                var applicantToken = await _applicantService.GetApplicantByTokenAsync(purlToken, userId);
                var applicantConsolidated = await _applicantService.GetConsolidatedDataByApplicantIdAsync((applicantToken is null || string.IsNullOrWhiteSpace(applicantToken.ApplicantId)) ? string.Empty : applicantToken.ApplicantId, userId);
                if (applicantConsolidated != null) {
                    applicantCompleteInfo.FirstName = applicantConsolidated.FirstName ?? "null";
                    applicantCompleteInfo.LastName = applicantConsolidated.LastName ?? "null";
                    if (applicantConsolidated.DOB != null) {
                        applicantCompleteInfo.BirthDate = Convert.ToDateTime(applicantConsolidated.DOB) != DateTime.MinValue ? Convert.ToDateTime(applicantConsolidated.DOB).ToString("yyyy-MM-dd") : null; // check applied on this null , remove node.
                    }
                    applicantCompleteInfo.EmailAddress = applicantConsolidated.Email ?? "null";
                    applicantCompleteInfo.Gender = applicantConsolidated.Gender ?? "null";
                } else {
                    applicantCompleteInfo.FirstName = "null";
                    applicantCompleteInfo.LastName = "null";
                    applicantCompleteInfo.BirthDate = null;
                    applicantCompleteInfo.EmailAddress = "null";
                    applicantCompleteInfo.Gender = "null";
                }
            } else if (applicantProfile == null) {
                applicantCompleteInfo.FirstName = "null";
                applicantCompleteInfo.LastName = "null";
                applicantCompleteInfo.BirthDate = null;
                applicantCompleteInfo.EmailAddress = "null";
                applicantCompleteInfo.Gender = "null";
            }

            return applicantCompleteInfo;
        }
        public async Task SaveUserPlansAsync(Quoting.DatabaseAdapter.Models.AQEApplicantPlan plan, long interactionId, List<Quoting.DatabaseAdapter.Models.AQEApplicantPlan> aqePlans = null, string userId = "", bool isAQE = false) {
            if (isAQE && aqePlans != null) {
                foreach (var aqePlan in aqePlans) {
                    aqePlan.UserId = userId;
                    aqePlan.QrsPlanId = null;
                    _qrsApplicantId = (await UserService.GetUserAsync(aqePlan.UserId)).QrsApplicantId;
                    aqePlan.QrsPlanId = await ManageQrsPlansForAQEAsync(aqePlan, interactionId, userId, true);
                    await _dbContext.Set<Quoting.DatabaseAdapter.Models.AQEApplicantPlan>().AddAsync(aqePlan);
                    await _dbContext.SaveChangesAsync();
                }
            } else {
                var prevUserPlan = await _dbContext.Set<Quoting.DatabaseAdapter.Models.AQEApplicantPlan>()
                        .Where(up => up.UserId == plan.UserId && up.Selected == true && up.IsDelete == false).ToListAsync();
                prevUserPlan.ForEach(async item => {
                    item.IsDelete = true;
                    item.Selected = false;
                    if (item.QrsPlanId > 0) {
                        await _qrsService.DeleteQrsDataAsync(Endpoints.ApplicantPlan, item.QrsPlanId, userId);
                    }
                });
                _qrsApplicantId = (await UserService.GetUserAsync(plan.UserId)).QrsApplicantId;
                plan.QrsPlanId = await ManageQrsPlansAsync(plan, interactionId, userId, true);
                await _dbContext.Set<Quoting.DatabaseAdapter.Models.AQEApplicantPlan>().AddAsync(plan);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<Quoting.DatabaseAdapter.Models.AQEApplicantPlan> GetUserPlanAsync(string userId)
                => await _dbContext.Set<Quoting.DatabaseAdapter.Models.AQEApplicantPlan>().Where(up => up.UserId == userId && up.Selected == true && up.IsDelete == false)
                    .OrderByDescending(year => year.EffectiveYear).FirstOrDefaultAsync();

        public async Task UpdateSavePlanAsync(Quoting.DatabaseAdapter.Models.AQEApplicantPlan plan, long interactionId, string userId) {
            var userPlan = await _dbContext.Set<Quoting.DatabaseAdapter.Models.AQEApplicantPlan>().FirstOrDefaultAsync(x =>
                                                                    x.PlanId == plan.PlanId &&
                                                                    x.UserId == plan.UserId &&
                                                                    x.IsDelete == false &&
                                                                    x.Selected);
            userPlan.EstimatedAnnualPrescriptionCost = plan.EstimatedAnnualPrescriptionCost;
            userPlan.EstimatedAnnualMedicalCost = plan.EstimatedAnnualMedicalCost;
            userPlan.QrsPlanId = await ManageQrsPlansAsync(userPlan, interactionId, userId);
            _dbContext.Set<Quoting.DatabaseAdapter.Models.AQEApplicantPlan>().Update(userPlan);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteUserPlanAsync(string userId, int id) {
            var userPlan = await _dbContext.Set<Quoting.DatabaseAdapter.Models.AQEApplicantPlan>().SingleOrDefaultAsync(up => up.UserId == userId && up.ID == id);
            if (userPlan != null) {
                userPlan.IsDelete = true;
                await _dbContext.SaveChangesAsync();
                if (userPlan.QrsPlanId > 0) {
                    await _qrsService.DeleteQrsDataAsync(Endpoints.ApplicantPlan, userPlan.QrsPlanId, userId);
                }
            }
        }

        public async Task<List<UserPlan>> GetSavedPlansAsync(string userId)
            => await _dbContext.Set<UserPlan>().Where(up => up.UserID == userId).ToListAsync();

        public async Task<bool> RemovePlansAsync(string userId) {
            var userPlans = await _dbContext.Set<Quoting.DatabaseAdapter.Models.AQEApplicantPlan>().Where(up => up.UserId == userId && up.Selected == true && up.IsDelete == false).ToListAsync();
            userPlans.ForEach(async (plan) => {
                plan.IsDelete = true;
                plan.Selected = false;
                if (plan.QrsPlanId > 0) {
                    await _qrsService.DeleteQrsDataAsync(Endpoints.ApplicantPlan, plan.QrsPlanId, userId);
                }
            });
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemovePlanAsync(int recordId, string userId) {
            var userPlan = await _dbContext.Set<Quoting.DatabaseAdapter.Models.AQEApplicantPlan>().FirstOrDefaultAsync(up => up.ID == recordId);
            userPlan.IsDelete = true;
            userPlan.Selected = false;
            await _dbContext.SaveChangesAsync();
            if (userPlan.QrsPlanId > 0) {
                await _qrsService.DeleteQrsDataAsync(Endpoints.ApplicantPlan, userPlan.QrsPlanId, userId);
            }
            return true;
        }

        #endregion

        #region Extra Help

        public async Task SetExtraHelpLevelAsync(string userId, int? extraHelpLevel, BrandType brandType) {
            var user = await _dbContext.Set<AspNetUser>().SingleOrDefaultAsync(u => u.Id == userId);
            var qrsApplicant = new QRSApplicants { ExtraHelpLevel = extraHelpLevel };
            if (user != null) {
                if (user.QrsApplicantId != null)
                    qrsApplicant.QrsApplicantId = (int)user.QrsApplicantId;
                user.ExtraHelpLevel = extraHelpLevel;
                await _dbContext.SaveChangesAsync();
            } else {
                var track = await _dbContext.Set<ApplicantTracking>().FirstOrDefaultAsync(x => x.UserId.ToLower().Equals(userId.ToLower()));
                qrsApplicant.QrsApplicantId = ConvertHelper.GetIntValue(track.QrsApplicantId);
            }
            await _qrsService.AddApplicantsAsync(qrsApplicant, brandType, userId);
        }

        public async Task<int?> GetExtraHelpLevel(string userId) {
            int? extraHelpLevel = null;
            var user = await _dbContext.Set<AspNetUser>().SingleOrDefaultAsync(u => u.Id == userId);
            if (user != null) {
                extraHelpLevel = user.ExtraHelpLevel;
            }
            return extraHelpLevel;
        }

        #endregion

        public async Task<AspNetUser> GetApplicantProfileAsync(string userId)
            => await _dbContext.Set<AspNetUser>().SingleOrDefaultAsync(u => u.Id == userId);

        private async Task<int> ManageQrsPlansAsync(Quoting.DatabaseAdapter.Models.AQEApplicantPlan plan, long interactionId, string userId, bool addEffectivePlan = false) {
            if (interactionId <= 0)
                return 0;
            var qrsPlan = PlanMapping(plan);
            qrsPlan.InteractionId = interactionId;
            var addedPlan = await _qrsService.AddPlanAsync(qrsPlan, interactionId, userId);
            if (addEffectivePlan) {
                await _qrsService.AddEffectivePlanAsync(addedPlan, _qrsApplicantId, userId);
            }
            return addedPlan;
        }

        private async Task<int> ManageQrsPlansForAQEAsync(Quoting.DatabaseAdapter.Models.AQEApplicantPlan plan, long interactionId, string userId, bool addEffectivePlan = false) {
            var qrsPlan = PlanMapping(plan);
            qrsPlan.InteractionId = interactionId;
            var addedPlan = await _qrsService.AddPlanAsync(qrsPlan, interactionId, userId);
            if (addEffectivePlan) {
                await _qrsService.AddEffectivePlanAsync(addedPlan, _qrsApplicantId, userId);
            }
            return addedPlan;
        }

        public async Task<bool> IsAnthemPlanAsync(string contractId) {
            var result = await _dbContext.Set<AnthemContract>().FirstOrDefaultAsync(x => x.Contract_ID.Equals(contractId));
            var isAnthemPlan = result != null;
            return isAnthemPlan;
        }
    }
}
