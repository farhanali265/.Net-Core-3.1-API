using Microsoft.EntityFrameworkCore;
using SQ.Senior.Clients.QrsService.Infrastructure.Constants;
using SQ.Senior.Clients.QrsService.Infrastructure.IQRSServices;
using SQ.Senior.Quoting.DatabaseAdapter.Abstractions;
using SQ.Senior.Quoting.DatabaseAdapter.Models;
using SQ.Senior.Quoting.External.Services.IServices;
using SQ.Senior.Quoting.External.Services.ViewModels.Prescription;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQ.Senior.Quoting.External.Services {
    public class PrescriptionService : BaseService, IPrescriptionService {
        private readonly ISQSeniorExternalQuotingDbContext _dbContext;
        private readonly IQrsPrescriptionService _qrsPrescriptionService;
        private readonly IUserService _userService;

        public PrescriptionService(ISQSeniorExternalQuotingDbContext dbContext, IQrsPrescriptionService qrsPrescriptionService, IUserService userService) : base(userService) {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _qrsPrescriptionService = qrsPrescriptionService;
        }

        public async Task<PrescriptionResponse> SavePrescriptionAsync(List<PrescriptionRequest> prescriptionsRequest, string userId, bool isAqe = false) {
            if (prescriptionsRequest is null || !prescriptionsRequest.Any())
                return null;

            var applicantTracking = await _userService.GetApplicantTrackingAsync(userId);
            var interactionId = applicantTracking?.InteractionId ?? 0;
            foreach (var prescription in prescriptionsRequest) {
                var prescriptionInfo = await _dbContext.Set<AgentVersionApplicantPrescription>().FirstOrDefaultAsync(cgx => cgx.UserId == userId && cgx.PrescriptionId == prescription.PrescriptionId && cgx.AmountPerThirtyDays == prescription.AmountPerThirtyDays && cgx.Package == prescription.Package && cgx.DosageId == prescription.DosageId);
                if (prescriptionInfo is null) {
                    var prescriptionRequestMapping = RequestPrescriptionToAgentVersionPrescriptionMapping(prescription);
                    if (prescriptionRequestMapping != null)
                        prescriptionRequestMapping.UserId = userId;

                    if (isAqe) {
                        prescription.QrsAgentPrescriptionId = await ManageQrsPrescriptionForAqeAsync(prescriptionRequestMapping, Convert.ToInt64(interactionId), userId);
                    } else {
                        prescription.QrsAgentPrescriptionId = await ManageQrsPrescriptionAsync(prescriptionInfo, Convert.ToInt64(interactionId), userId);
                    }
                    await _dbContext.Set<AgentVersionApplicantPrescription>().AddAsync(prescriptionRequestMapping);
                } else {
                    prescriptionInfo.QrsAgentPrescriptionId = await ManageQrsPrescriptionAsync(prescriptionInfo, Convert.ToInt64(interactionId), userId);
                }
                await _dbContext.SaveChangesAsync();
                return PrescriptionResponseMapping(prescriptionsRequest.FirstOrDefault());
            }
            return null;
        }

        public async Task<PrescriptionResponse> EditPrescriptionAsync(PrescriptionRequest prescription, string userId, int prescriptionId) {
            if (prescription is null)
                return null;

            var editPrescription = await _dbContext.Set<AgentVersionApplicantPrescription>().FirstOrDefaultAsync(cgx => cgx.UserId == userId && cgx.Id == prescriptionId);
            if (editPrescription is null)
                return null;

            var applicantTracking = await _userService.GetApplicantTrackingAsync(userId);
            var interactionId = applicantTracking?.InteractionId ?? 0;
            editPrescription.PrescriptionName = prescription.PrescriptionName;
            editPrescription.DosageId = prescription.DosageId;
            editPrescription.AmountPerThirtyDays = prescription.AmountPerThirtyDays;
            editPrescription.Package = prescription.Package;
            editPrescription.PrescriptionLabel = prescription.PrescriptionLabel;
            editPrescription.PackageName = prescription.PackageName;
            editPrescription.QrsAgentPrescriptionId = await ManageQrsPrescriptionAsync(editPrescription, Convert.ToInt64(interactionId), userId);
            await _dbContext.SaveChangesAsync();
            return PrescriptionResponseMapping(prescription);
        }

        public async Task<List<PrescriptionResponse>> GetPrescriptionAsync(string userId) {
            var prescriptions = await (from res in _dbContext.Set<AgentVersionApplicantPrescription>()
                                       where res.UserId == userId
                                       select res).ToListAsync();

            var prescriptionsResponse = new List<PrescriptionResponse>();
            foreach (var prescription in prescriptions) {
                prescriptionsResponse.Add(AgentVersionApplicantPrescriptionMapping(prescription));
            }
            return prescriptionsResponse;
        }

        public async Task<bool> DeletePrescriptionAsync(int prescriptionId, string userId) {
            var prescription = await _dbContext.Set<AgentVersionApplicantPrescription>().FirstOrDefaultAsync(cgx => cgx.UserId == userId && cgx.Id == prescriptionId);
            if (prescription == null)
                return false;
            if (prescription.QrsAgentPrescriptionId == 0 || prescription.QrsAgentPrescriptionId < 1)
                return false;

            await _qrsPrescriptionService.DeletePrescriptionAsync(Endpoints.ApplicantPrescription, prescription.QrsAgentPrescriptionId, userId);
            _dbContext.Set<AgentVersionApplicantPrescription>().Remove(prescription);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        private async Task<long> ManageQrsPrescriptionAsync(AgentVersionApplicantPrescription prescription, long interactionId, string userId) {
            if (interactionId <= 0)
                return interactionId;

            var qrsPrescriptions = PrescriptionMapping(prescription);
            qrsPrescriptions.InteractionId = interactionId;
            return await _qrsPrescriptionService.SavePrescriptionAsync(qrsPrescriptions, interactionId, userId);
        }

        private async Task<long> ManageQrsPrescriptionForAqeAsync(AgentVersionApplicantPrescription prescription, long interactionId, string userId) {
            if (interactionId <= 0)
                return interactionId;

            var qrsPrescriptions = PrescriptionMapping(prescription);
            qrsPrescriptions.InteractionId = interactionId;
            return await _qrsPrescriptionService.SavePrescriptionAsync(qrsPrescriptions, interactionId, userId);
        }
    }
}
