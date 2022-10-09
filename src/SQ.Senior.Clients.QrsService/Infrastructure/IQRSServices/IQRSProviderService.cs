using SQ.Senior.Clients.QrsService.Models;
using System.Threading.Tasks;

namespace SQ.Senior.Clients.QrsService.Infrastructure.IQRSServices {
    public interface IQrsProviderService {
        Task<QRSSearchProvider> SearchProviderAsync(QRSProviderFilterCriteria searchProviderCriteria, int pageNo, int limit, string userId = "");
        Task<int> AddProviderAsync(QRSProvider provider, long interactionId, string userId);
    }
}
