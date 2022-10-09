using SQ.Senior.Clients.DrxServices.Models;
using SQ.Senior.Clients.QrsService.Models;
using SQ.Senior.Quoting.DatabaseAdapter.Models;
using SQ.Senior.Quoting.External.Services.ViewModels.Plan;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SQ.Senior.Quoting.External.Services.IServices {
    public interface IUserPlanService {
        Task SaveUserPlansAsync(Quoting.DatabaseAdapter.Models.AQEApplicantPlan plan, long interactionId, List<Quoting.DatabaseAdapter.Models.AQEApplicantPlan> aqePlans = null, string userId = "", bool isAQE = false);
        Task<Quoting.DatabaseAdapter.Models.AQEApplicantPlan> GetUserPlanAsync(string userId);
        Task UpdateSavePlanAsync(Quoting.DatabaseAdapter.Models.AQEApplicantPlan plan, long interactionId, string userId);
        Task DeleteUserPlanAsync(string userId, int id);
        Task<List<UserPlan>> GetSavedPlansAsync(string userId);
        Task<bool> RemovePlansAsync(string userId);
        Task<bool> RemovePlanAsync(int recordId, string userId);
        Task SetExtraHelpLevelAsync(string userId, int? extraHelpLevel, BrandType brandType);
        Task<int?> GetExtraHelpLevel(string userId);
        Task<AspNetUser> GetApplicantProfileAsync(string userId);
        Task<bool> IsAnthemPlanAsync(string contractId);
        Task<Plans> GetDrxPlansAsync(GetPlansRequest getPlansRequest);
        Task<ApplicantCompleteInfo> GetApplicantInfoAsync(string userId, string purlToken);
    }
}
