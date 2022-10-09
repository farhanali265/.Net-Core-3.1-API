using SQ.Senior.Clients.QrsService.Infrastructure.Constants;
using SQ.Senior.Clients.QrsService.Infrastructure.IQRSServices;
using SQ.Senior.Clients.QrsService.Models;
using System;
using System.Threading.Tasks;

namespace SQ.Senior.Clients.QrsService.Infrastructure.Services {
    public class QrsPrescriptionService : IQrsPrescriptionService {
        private readonly IQrsApiClient _qrsApiClient;

        public QrsPrescriptionService(IQrsApiClient qrsApiClient) {
            _qrsApiClient = qrsApiClient ?? throw new ArgumentNullException(nameof(IQrsApiClient));
        }

        public async Task<QRSPrescription> GetPrescriptionAsync(long qrsPrescriptionId, string userId) {
            var endPoint = $"{Endpoints.ApplicantPrescription}/{qrsPrescriptionId}";
            try {
                return await _qrsApiClient.GetAsync<QRSPrescription>(endPoint, userId);
            } catch (Exception ex) {
                ApiLogService.LogException(ex, userId);
                return null;
            }
        }

        public async Task<int> SavePrescriptionAsync(QRSPrescription prescription, long interactionId, string userId) {
            var endPoint = string.Format(Endpoints.Prescription, interactionId);
            var qrsPrescriptionId = 0;
            QRSPrescription qrsPrescription = null;
            var isNewPrescription = true;
            try {
                if (prescription!=null && prescription.QrsPrescriptionId > 0) {
                    var existingPrescription = await GetPrescriptionAsync((int)prescription.QrsPrescriptionId, userId);
                    if (existingPrescription != null) {
                        isNewPrescription = false;
                        var modifiedPrescription = prescription;
                        modifiedPrescription.Id = existingPrescription.Id;
                        modifiedPrescription.IsDeleted = false;
                        modifiedPrescription.DateModified = DateTime.UtcNow;
                        modifiedPrescription.DateDeleted = null;
                        qrsPrescription = await _qrsApiClient.PatchRequestAsync(Endpoints.ApplicantPrescription, (int)prescription.QrsPrescriptionId, modifiedPrescription, userId);
                    }
                }
                if (isNewPrescription) {
                    prescription.DateCreated = DateTime.UtcNow;
                    qrsPrescription = await _qrsApiClient.PostRequestAsync(endPoint, prescription, userId);
                }

                if (qrsPrescription != null && qrsPrescription.Id > 0) {
                    qrsPrescriptionId = qrsPrescription.Id;
                }
            } catch (Exception ex) {
                ApiLogService.LogException(ex, userId);
            }
            return qrsPrescriptionId;
        }

        public async Task DeletePrescriptionAsync(string endPoint, long? recordId, string userId) {
            try {
                if (recordId != null)
                    await _qrsApiClient.DeleteRequestAsync(endPoint, (int)recordId, userId);
            } catch (Exception ex) {
                ApiLogService.LogException(ex, userId);
            }
        }
    }
}
