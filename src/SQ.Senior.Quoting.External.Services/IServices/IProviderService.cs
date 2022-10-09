using SQ.Senior.Clients.QrsService.Models;
using SQ.Senior.Quoting.DatabaseAdapter.Models;
using SQ.Senior.Quoting.External.Services.ViewModels.Plan;
using SQ.Senior.Quoting.External.Services.ViewModels.Providers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SQ.Senior.Quoting.External.Services.IServices {
    public interface IProviderService {
        Task<IEnumerable<ProviderResponse>> GetApplicantProviderAsync(string userId, bool isActive);
        Task<QRSSearchProvider> SearchProviders(SearchProvidersRequest searchRequest);
        Task<bool> SaveProvidersAsync(int nationalProviderId, string userId, BrandType brandType, List<ApplicantProvider> applicantProviders = null, bool isAQE = false);
        Task<int> DeleteProviderAsync(string userId, int doctorId);
        Task<ApplicantProvider> GetProviderByNpiNumberAsync(int nationalProviderId);
    }
}
