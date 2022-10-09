using SQ.Senior.Clients.DrxServices.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SQ.Senior.Clients.DrxServices.Infrastructure.Services {
    public interface IDrxApiClient {
        Task<List<Prescription>> SearchPrescriptionsAsync(string prescriptionKeyword);
        Task<PrescriptionInfo> GetPrescriptionInfo(int id, string drxAccessToken);
        PrescriptionInfo GetPrescriptionInfo(string PrescriptionInfoAsXml);
        Task<List<Pharmacy>> SearchDrxPharmaciesAsync(string zipCode, string drxAccessToken, string pharmacyDistance, string pharmacyName);
        Task<List<Pharmacy>> GetPharmacyAutoComplete(string zipCode, string planId, string drxAccessToken);
        Task<string> AddSessionPharmacy(string drxSession, string drxAccessToken, string pharmacyJson);
        Task<string> DeleteSessionPharmacy(string drxSession, string dRXAccessToken);
        Task<string> GetPlans(string sessionId, string zip, string fips, string planYear, string dRXAccessToken);
        Task<MedicarePlanDetail> GetPlanDetail(string planId, string sessionId, string zip, string fips, string effectiveDate, string dRXAccessToken);
        Task<List<List<TierInfo>>> GetFormularyPharmacy(string planId, string sessionId, string zip, string fips, string effectiveDate, string dRXAccessToken);
        FormularyTierFilters GetFormularyTypeDetail(int formularyTypeDetailId);
        Task<Plan> GetPlanDetailExtended(string planId, string sessionId, string zip, string fips, string effectiveDate, string dRXAccessToken);
        Task<string> PlanEnroll(ApplicationUser user, AQEApplicantPlan plan, string sessionId, string dRXAccessToken, string fips, string zip, string userID, string arsApplicantId, string token);
    }
}
