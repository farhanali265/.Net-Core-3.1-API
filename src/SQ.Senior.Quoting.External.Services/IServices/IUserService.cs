using SQ.Senior.Clients.QrsService.Models;
using SQ.Senior.Quoting.DatabaseAdapter.Models;
using System;
using System.Threading.Tasks;

namespace SQ.Senior.Quoting.External.Services.IServices {
    public interface IUserService {
        Task<AspNetUser> GetUserAsync(string userId);
        Task<AspNetUser> GetUserByTokenAsync(string token);
        Task<string> VerifyUserAsync(string email);
        Task<bool> CheckEmailAsync(string email);
        Task<bool> CheckDateOfBirthAsync(DateTime? dateOfBirth, string email);
        Task SetTemporaryPasswordAsync(string email, string password);
        Task<long?> UpdateUserApplicantIdAsync(string userId, long applicantId);
        Task<string> UpdateUserPhoneNumber(string userId, string phoneNumber);
        Task<long> ManageUserInteractionSessionAsync(BrandType brandType, string userId);
        Task<AspNetUser> GetUserByEmailAsync(string userEmail);
        Task<ApplicantTracking> SaveNewApplicantTrackingAsync(ApplicantTracking applicantTracking);
        Task<ApplicantTracking> SaveApplicantTrackingAsync(ApplicantTracking applicantTracking);
        Task<ApplicantTracking> UpdateApplicantTrackingZipFipsAsync(ApplicantTracking applicantTracking);
        Task<ApplicantTracking> UpdateApplicantTrackingForScreenSizeAsync(ApplicantTracking applicantTracking);
        Task UpdateUserAsync(AspNetUser user);
        Task<ApplicantTracking> GetApplicantTrackingAsync(string userId);
    }
}
