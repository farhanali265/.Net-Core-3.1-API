using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SQ.Senior.Clients.QrsService.Infrastructure.Constants;
using SQ.Senior.Clients.QrsService.Infrastructure.IQRSServices;
using SQ.Senior.Quoting.DatabaseAdapter.Abstractions;
using SQ.Senior.Quoting.DatabaseAdapter.Models;
using SQ.Senior.Quoting.External.Services.IServices;
using SQ.Senior.Quoting.External.Services.ViewModels.Pharmacy;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SQ.Senior.Quoting.External.Services {
    public class PharmacyService : BaseService, IPharmacyService {
        private readonly ILogger<PharmacyService> _logger;
        private readonly ISQSeniorExternalQuotingDbContext _dbContext;
        private readonly IQrsService _qrsService;
        private readonly IUserService _userService;

        public PharmacyService(ILogger<PharmacyService> logger, ISQSeniorExternalQuotingDbContext dbContext, IUserService userService, IQrsService qrsService) : base(userService) {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _qrsService = qrsService;
            _userService = userService;
        }

        public async Task<bool> DeletePharmacyAsync(string userId, string pharmacyId) {
            var userPharmacy = await _dbContext.Set<Pharmacy>().FirstOrDefaultAsync(x => x.UserId == userId && x.PharmacyDrxId == pharmacyId && x.IsDeleted == false);
            if (userPharmacy == null)
                return false;

            userPharmacy.IsDeleted = true;
            if (userPharmacy.QrsPharmacyId > 0) {
                await _qrsService.DeleteQrsDataAsync(Endpoints.ApplicantPharmacy, userPharmacy.QrsPharmacyId, userId);
            }

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task UpdateUserPharmacyStatusAsync(string userId) {

            var user = await _userService.GetUserAsync(userId);
            if (user != null) {
                user.ProviderDoesNotApply = false;
                await _userService.UpdateUserAsync(user);
            }
        }

        public async Task<bool> DeletePharmacyAsync(string userId) {
            var userPharmacy = await _dbContext.Set<Pharmacy>().FirstOrDefaultAsync(x => x.UserId == userId && x.IsDeleted == false);
            if (userPharmacy == null)
                return false;
            userPharmacy.IsDeleted = true;
            if (userPharmacy.QrsPharmacyId > 0) {
                await _qrsService.DeleteQrsDataAsync(Endpoints.ApplicantPharmacy, userPharmacy.QrsPharmacyId, userId);
            }
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<PharmacyResponse> GetPharmacyAsync(string userId) {
            var pharmacy = await _dbContext.Set<Pharmacy>().FirstOrDefaultAsync(x => x.UserId == userId && x.IsDeleted == false);
            return GetPharmacyResponseMapping(pharmacy);
        }

        public async Task<PharmacyResponse> SavePharmacyAsync(string userId, SavePharmacyRequest savePharmacyRequest, bool isAQE = false) {

            if (savePharmacyRequest is null || string.IsNullOrWhiteSpace(userId))
                return null;

            var pharmacy = GetPharmacyMapping(savePharmacyRequest);
            var applicantTracking = await _userService.GetApplicantTrackingAsync(userId);
            var interactionId = applicantTracking?.InteractionId ?? 0;
            if (isAQE) {
                pharmacy.Id = Guid.NewGuid();
                pharmacy.UserId = userId;
                pharmacy.IsDeleted = false;
                pharmacy.Dated = DateTime.UtcNow;
                pharmacy.QrsPharmacyId = await ManageQrsPharmacyForAQEAsync(pharmacy, interactionId, userId);
                await _dbContext.Set<Pharmacy>().AddAsync(pharmacy);
                await _dbContext.SaveChangesAsync();
                return GetPharmacyResponseMapping(pharmacy);
            }

            var userPharmacies = await _dbContext.Set<Pharmacy>().Where(x => x.UserId == userId).ToListAsync();
            if (userPharmacies != null && userPharmacies.Any()) {
                foreach (var userPharmacy in userPharmacies) {
                    userPharmacy.IsDeleted = true;
                    userPharmacy.Dated = DateTime.UtcNow;
                    if (userPharmacy.QrsPharmacyId > 0) {
                        await _qrsService.DeleteQrsDataAsync(Endpoints.ApplicantPharmacy, userPharmacy.QrsPharmacyId, userId);
                    }
                    await _dbContext.SaveChangesAsync();
                }
                var currentPharmacy = userPharmacies.FirstOrDefault(x => x.PharmacyDrxId == pharmacy.PharmacyDrxId);
                if (currentPharmacy != null) {
                    currentPharmacy.IsDeleted = false;
                    currentPharmacy.Dated = DateTime.UtcNow;
                    currentPharmacy.QrsPharmacyId = await ManageQrsPharmacyAsync(currentPharmacy, interactionId, userId);
                    await _dbContext.SaveChangesAsync();
                    return GetPharmacyResponseMapping(currentPharmacy);
                }
            }
            pharmacy.UserId = userId;
            pharmacy.Id = Guid.NewGuid();
            pharmacy.Dated = DateTime.UtcNow;
            pharmacy.IsDeleted = false;
            pharmacy.QrsPharmacyId = await ManageQrsPharmacyAsync(pharmacy, interactionId, userId);
            await _dbContext.Set<Pharmacy>().AddAsync(pharmacy);
            await _dbContext.SaveChangesAsync();
            return GetPharmacyResponseMapping(pharmacy);
        }

        private async Task<int> ManageQrsPharmacyAsync(Pharmacy pharmacy, long interactionId, string userId) {
            if (interactionId > 0) {
                var qrsPharmacy = PharmacyMapping(pharmacy);
                qrsPharmacy.InteractionId = interactionId;
                return await _qrsService.AddPharmacyAsync(qrsPharmacy, interactionId, userId);
            }
            return 0;
        }

        private async Task<int> ManageQrsPharmacyForAQEAsync(Pharmacy pharmacy, long interactionId, string userId) {
            if (interactionId > 0) {
                var qrsPharmacy = PharmacyMapping(pharmacy);
                qrsPharmacy.InteractionId = interactionId;
                return await _qrsService.AddPharmacyAsync(qrsPharmacy, interactionId, userId);
            }
            return 0;
        }
    }
}
