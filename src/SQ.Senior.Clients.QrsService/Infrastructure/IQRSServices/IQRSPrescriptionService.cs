using SQ.Senior.Clients.QrsService.Models;
using System.Threading.Tasks;

namespace SQ.Senior.Clients.QrsService.Infrastructure.IQRSServices {
    public interface IQrsPrescriptionService {
        Task<QRSPrescription> GetPrescriptionAsync(long qrsPrescriptionId, string userId);
        Task<int> SavePrescriptionAsync(QRSPrescription prescription, long interactionId, string userId);
        Task DeletePrescriptionAsync(string endPoint, long? recordId, string userId);
    }
}
