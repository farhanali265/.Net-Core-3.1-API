using SQ.Senior.Clients.QrsService.Models;
using SQ.Senior.Quoting.DatabaseAdapter.Models;
using System.Threading.Tasks;

namespace SQ.Senior.Quoting.External.Services.IServices {
    public interface IApplicantService {
        Task<QRSApplicantsTokens> GetApplicantByTokenAsync(string token, string userId);
        Task<QRSApplicants> GetApplicantByApplicantIdAsync(string applicantId, string userId);
        Task<QRSApplicants> GetConsolidatedDataByApplicantIdAsync(string applicantId, string userId);
        Task<ApplicantTracking> GetApplicantZipFipsByApplicantIdAsync(string applicantId, string ZipCode, string userId);
        Task<long> GetCurrentInteractionIdAsync(string userId, BrandType brandType);
        Task<ApplicantTracking> SaveApplicantTrackingPurlAsync(PurlParams purlParams, string userId, BrandType brandType, string purlType);
    }
}
