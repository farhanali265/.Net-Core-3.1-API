using SQ.Senior.Clients.QrsService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SQ.Senior.Clients.QrsService.Infrastructure.IQRSServices {
    public interface IQrsService {
        Task<ApplicantInteractionResponse> AddApplicantsAsync(QRSApplicants applicant, BrandType brandType, string userId);
        long AddInteraction(long applicantId, BrandType brandType, string userId);
        Task<List<ContactType>> GetContactTypes(string userId);
        Task<int> AddZipFipCode(QRSContact contact, long interactionId, string userId);
        Task AddContactsAsync(QRSContact contact, string contactType, long interactionId, string userId, bool isAQE = false);
        Task AddContactsAsyncGeneric(QRSContact contact, long interactionId, string userId);
        Task<QRSApplicants> GetApplicant(int qrsApplicantId, string userId);
        Task<QRSContact> GetContact(int qrsContactId, string userId);
        Task<QRSProvider> GetProvider(long qrsProviderId, string userId);
        Task<QRSPharmacy> GetPharmacy(int qrsPharmacyId, string userId);
        Task<QRSPlan> GetPlan(long qrsPlanId, string userId);
        Task<int> AddPharmacyAsync(QRSPharmacy pharmacy, long interactionId, string userId);
        Task<int> AddPlanAsync(QRSPlan plan, long interactionId, string userId);
        Task<int> AddEffectivePlanAsync(int planId, long? qrsApplicantId, string userId);
        Task DeleteQrsDataAsync(string endPoint, long? recordId, string userId);
        Task<QRSApplicantsTokens> GetApplicantByTokenAsync(string endPoint, string token, string userId);
        Task<QRSApplicants> GetApplicantByApplicantIdAsync(string endPoint, string applicantId, string userId);
        Task<QRSApplicants> GetConsolidatedDataByApplicantIdAsync(string endPoint, string applicantId, string userId);
    }
}
