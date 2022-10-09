using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SQ.Senior.Clients.QrsService.Infrastructure.IQRSServices;
using SQ.Senior.Clients.QrsService.Models;
using SQ.Senior.Quoting.DatabaseAdapter.Abstractions;
using SQ.Senior.Quoting.DatabaseAdapter.Models;
using SQ.Senior.Quoting.External.Services.IServices;
using System;
using System.Threading.Tasks;

namespace SQ.Senior.Quoting.External.Services {
    public class UserService : IUserService {
        private readonly ILogger<UserService> _logger;
        private readonly ISQSeniorExternalQuotingDbContext _dbContext;
        private readonly IQrsService _qrsService;

        public UserService(ILogger<UserService> logger, ISQSeniorExternalQuotingDbContext dbContext, IQrsService qrsService) {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _qrsService = qrsService;
        }

        public async Task<AspNetUser> GetUserAsync(string userId)
            => await _dbContext.Set<AspNetUser>().FindAsync(userId);

        public async Task<AspNetUser> GetUserByTokenAsync(string token)
                => await _dbContext.Set<AspNetUser>().FirstOrDefaultAsync(x => x.EntryToken.Equals(token));

        public async Task<string> VerifyUserAsync(string email) {
            var userExist = await _dbContext.Set<AspNetUser>().FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());
            return userExist == null ? "InValid" : "Valid";
        }

        public async Task<bool> CheckEmailAsync(string email) {
            var userExist = await _dbContext.Set<AspNetUser>().FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());
            return userExist != null;
        }

        public async Task<bool> CheckDateOfBirthAsync(DateTime? dateOfBirth, string email) {
            try {
                var userExist = await _dbContext.Set<AspNetUser>()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower() && x.DateOfBirth == dateOfBirth);
                return userExist == null;
            } catch (Exception e) {
                _logger.LogError(e.ToString());
                return false;
            }
        }

        public async Task SetTemporaryPasswordAsync(string email, string password) {
            var user = await _dbContext.Set<AspNetUser>().SingleOrDefaultAsync(us => us.Email == email);
            if (user != null) {
                user.EmailConfirmed = true;
                user.PasswordHash = password;
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<long?> UpdateUserApplicantIdAsync(string userId, long applicantId) {
            var user = await _dbContext.Set<AspNetUser>().FirstOrDefaultAsync(us => us.Id.Equals(userId));
            if (user == null)
                return 0;
            user.QrsApplicantId = (int)applicantId;
            await _dbContext.SaveChangesAsync();
            return applicantId;
        }

        public async Task<string> UpdateUserPhoneNumber(string userId, string phoneNumber) {
            var user = await _dbContext.Set<AspNetUser>().FirstOrDefaultAsync(us => us.Id.Equals(userId));
            if (user == null)
                return string.Empty;
            user.EntryPhoneNumber = phoneNumber;
            await _dbContext.SaveChangesAsync();
            return phoneNumber;
        }

        public async Task<long> ManageUserInteractionSessionAsync(BrandType brandType, string userId) {
            var user = await GetUserAsync(userId);
            long interactionId = 0;
            if (user == null)
                return interactionId;
            if (user.QrsApplicantId == null || user.QrsApplicantId == 0) {
                var applicationRequest = new QRSApplicants {
                    FirstName = user.FirstName,
                    MiddleInitial = user.MiddleInitial,
                    LastName = user.LastName,
                    Email = user.Email,
                    DateCreated = user.CreatedDate
                };
                var qrsResponse = await _qrsService.AddApplicantsAsync(applicationRequest, brandType, userId);
                await UpdateUserApplicantIdAsync(userId, qrsResponse.ApplicantId);
                interactionId = (int)qrsResponse.InteractionId;
            } else {
                interactionId = _qrsService.AddInteraction((int)user.QrsApplicantId, brandType, userId);
            }
            return interactionId;
        }

        public async Task<AspNetUser> GetUserByEmailAsync(string userEmail)
            => await _dbContext.Set<AspNetUser>().FirstOrDefaultAsync(x => x.Email.ToLower().Equals(userEmail.ToLower()));

        public async Task<ApplicantTracking> SaveNewApplicantTrackingAsync(ApplicantTracking applicantTracking) {
            var track = await _dbContext.Set<ApplicantTracking>().FirstOrDefaultAsync(x => x.UserId.ToLower().Equals(applicantTracking.UserId.ToLower()));
            if (track != null)
                return applicantTracking;
            var applicant = await _dbContext.Set<ApplicantTracking>().AddAsync(applicantTracking);
            await _dbContext.SaveChangesAsync();
            return applicantTracking;
        }

        public async Task<ApplicantTracking> SaveApplicantTrackingAsync(ApplicantTracking applicantTracking) {
            if (applicantTracking is null)
                return applicantTracking;

            var existingApplicant = await _dbContext.Set<ApplicantTracking>().FirstOrDefaultAsync(x => x.UserId.Equals(Convert.ToString(applicantTracking.UserId)));
            if (existingApplicant is null) {
                await _dbContext.Set<ApplicantTracking>().AddAsync(applicantTracking);
                await _dbContext.SaveChangesAsync();
                return applicantTracking;
            } else {
                existingApplicant.ScreenSize = existingApplicant.ScreenSize ?? applicantTracking.ScreenSize;
                existingApplicant.DeviceType = existingApplicant.DeviceType ?? applicantTracking.DeviceType;
                existingApplicant.ZIPCode = existingApplicant.ZIPCode ?? applicantTracking.ZIPCode;
                existingApplicant.FipsCode = existingApplicant.FipsCode ?? applicantTracking.FipsCode;
                existingApplicant.EntrySource = existingApplicant.EntrySource ?? applicantTracking.EntrySource;
                existingApplicant.EntryToken = existingApplicant.EntryToken ?? applicantTracking.EntryToken;
                existingApplicant.EntryKeyword = existingApplicant.EntryKeyword ?? applicantTracking.EntryKeyword;
                existingApplicant.AccountId = existingApplicant.AccountId ?? applicantTracking.AccountId;
                existingApplicant.IndividualId = existingApplicant.IndividualId ?? applicantTracking.IndividualId;
                await _dbContext.SaveChangesAsync();
                return existingApplicant;
            }
        }

        public async Task<ApplicantTracking> UpdateApplicantTrackingZipFipsAsync(ApplicantTracking applicantTracking) {
            var track = await _dbContext.Set<ApplicantTracking>().FirstOrDefaultAsync(x => x.UserId.ToLower().Equals(applicantTracking.UserId.ToLower()));
            if (track == null) {
                await _dbContext.Set<ApplicantTracking>().AddAsync(applicantTracking);
                await _dbContext.SaveChangesAsync();
                return applicantTracking;
            }
            track.ZIPCode = !string.IsNullOrEmpty(applicantTracking.ZIPCode) ? applicantTracking.ZIPCode : track.ZIPCode;
            track.FipsCode = !string.IsNullOrEmpty(applicantTracking.FipsCode) ? applicantTracking.FipsCode : track.FipsCode;
            await _dbContext.SaveChangesAsync();
            return new ApplicantTracking();
        }

        public async Task<ApplicantTracking> UpdateApplicantTrackingForScreenSizeAsync(ApplicantTracking applicantTracking) {
            var track = await _dbContext.Set<ApplicantTracking>().FirstOrDefaultAsync(x => x.UserId.ToLower().Equals(applicantTracking.UserId.ToLower()));
            if (track == null)
                return null;
            track.ScreenSize = applicantTracking.ScreenSize > 0 ? applicantTracking.ScreenSize : track.ScreenSize;
            track.DeviceType = !string.IsNullOrEmpty(applicantTracking.DeviceType) ? applicantTracking.DeviceType : track.DeviceType;
            await _dbContext.SaveChangesAsync();
            return track;
        }

        public async Task UpdateUserAsync(AspNetUser user) {
            var existingUser = await _dbContext.Set<AspNetUser>().FirstOrDefaultAsync(x => x.Id.Equals(user.Id));
            if (user != null) {
                existingUser.PrescriptionDoesNotApply = user.PrescriptionDoesNotApply;
                existingUser.PharmacyDoesNotApply = user.PharmacyDoesNotApply;
                existingUser.ProviderDoesNotApply = user.ProviderDoesNotApply;
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<ApplicantTracking> GetApplicantTrackingAsync(string userId)
            => await _dbContext.Set<ApplicantTracking>().FirstOrDefaultAsync(x => x.UserId.Equals(userId));
    }
}
