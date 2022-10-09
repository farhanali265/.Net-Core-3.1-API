using SQ.Senior.Quoting.External.Services.ViewModels.Pharmacy;
using System.Threading.Tasks;

namespace SQ.Senior.Quoting.External.Services.IServices {
    public interface IPharmacyService {
        Task<bool> DeletePharmacyAsync(string userId, string pharmacyId);
        Task<bool> DeletePharmacyAsync(string userId);
        Task<PharmacyResponse> GetPharmacyAsync(string userId);
        Task<PharmacyResponse> SavePharmacyAsync(string userId, SavePharmacyRequest savePharmacyRequest, bool isAQE = false);
        Task UpdateUserPharmacyStatusAsync(string userId);
    }
}
