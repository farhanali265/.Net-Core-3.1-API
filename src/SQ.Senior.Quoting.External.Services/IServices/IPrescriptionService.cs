using SQ.Senior.Quoting.External.Services.ViewModels.Prescription;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace SQ.Senior.Quoting.External.Services.IServices {
    public interface IPrescriptionService {
        Task<PrescriptionResponse> SavePrescriptionAsync(List<PrescriptionRequest> prescriptionsRequest, string userId, bool isAqe = false);
        Task<bool> DeletePrescriptionAsync(int prescriptionId, string userId);
        Task<PrescriptionResponse> EditPrescriptionAsync(PrescriptionRequest prescriptionRequest, string userId,int prescriptionId);
        Task<List<PrescriptionResponse>> GetPrescriptionAsync(string userId);
    }
}
