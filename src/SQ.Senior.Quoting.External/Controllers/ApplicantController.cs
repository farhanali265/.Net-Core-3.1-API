using Microsoft.AspNetCore.Mvc;
using SQ.Senior.Clients.QrsService.Infrastructure.IQRSServices;
using SQ.Senior.Clients.QrsService.Models;
using SQ.Senior.Quoting.DatabaseAdapter.Models;
using SQ.Senior.Quoting.External.Helpers;
using SQ.Senior.Quoting.External.Infrastructure.Constants;
using SQ.Senior.Quoting.External.Response;
using SQ.Senior.Quoting.External.Services.IServices;
using System;
using System.Threading.Tasks;

namespace SQ.Senior.Quoting.External.Controllers {
    public class ApplicantController : BaseController {
        private readonly IUserService _userService;
        private readonly IApplicantService _applicantService;
        private readonly ApplicantResponse _applicantResponse;
        private readonly ApplicantTracking _applicantTracking;
        private readonly IQrsService _qrsService;
        private readonly QRSApplicantsTokens applicantToken;
        private readonly PurlResponse _purlResponse;

        public ApplicantController(IUserService userService, IApplicantService applicantService, IQrsService qrsService) {
            _userService = userService;
            _applicantService = applicantService;
            _qrsService = qrsService;
            _applicantTracking = new ApplicantTracking();
            _applicantResponse = new ApplicantResponse();
            applicantToken = new QRSApplicantsTokens();
            _purlResponse = new PurlResponse();

        }

        [HttpGet("ApplicantByToken")]
        public async Task<IActionResult> ApplicantByToken(string token, string userId) {
            var applicantToken = await _applicantService.GetApplicantByTokenAsync(token, userId);
            if (applicantToken == null) {
                _applicantResponse.Status = ApplicantStatus.InvalidToken;
                return Ok(_applicantResponse.Status);
            }
            var applicant = await _applicantService.GetApplicantByApplicantIdAsync(applicantToken.ApplicantId, userId);
            if (applicant == null) {
                _applicantResponse.Status = ApplicantStatus.NotFound;
                return Ok(_applicantResponse.Status);
            }
            _applicantResponse.Status = applicant?.Email ?? _applicantResponse.Status;
            return Ok(_applicantResponse.Status);
        }

        [HttpGet("ApplicantByTokenAQE")]
        public async Task<IActionResult> ApplicantByTokenAQE(string token, string userId) {
            var applicant = new QRSApplicants();
            var applicantToken = await _applicantService.GetApplicantByTokenAsync(token, userId);
            if (applicantToken == null) {
                _applicantResponse.Status = ApplicantStatus.InvalidToken;
                return Ok(_applicantResponse.Status);
            }
            applicant = await _applicantService.GetApplicantByApplicantIdAsync(applicantToken.ApplicantId, userId);
            if (applicant == null) {
                return Ok(ApplicantStatus.NotFound);
            }
            if (applicant != null) {
                var isEmailExists = await _userService.CheckEmailAsync(applicant.Email);
                _applicantResponse.Status = isEmailExists ? $"{ApplicantStatus.Exists},{applicant.Email}" : applicant.Email;
            }
            return Ok(_applicantResponse.Status);
        }

        [HttpGet("VerifyEmailByQRS")]
        public async Task<IActionResult> VerifyEmailByQRS(string token, string email, string userId) {
            var applicantToken = await _applicantService.GetApplicantByTokenAsync(token, userId);
            if (applicantToken == null) {
                _applicantResponse.Status = ApplicantStatus.InvalidToken;
                return Ok(_applicantResponse);
            }
            var applicant = await _applicantService.GetApplicantByApplicantIdAsync(applicantToken.ApplicantId, userId);
            if (applicant == null) {
                _applicantResponse.Status = ApplicantStatus.Mismatch;
                return Ok(_applicantResponse);
            }
            var isEmailExists = await _userService.CheckEmailAsync(email);

            _applicantResponse.Status = !string.IsNullOrEmpty(applicant.Email) && applicant.Email.ToLower().Equals(email.ToLower()) && !isEmailExists
                ? ApplicantStatus.Valid : (isEmailExists ? ApplicantStatus.Exists : ApplicantStatus.Mismatch);

            _applicantResponse.Applicant = applicant;
            return Ok(_applicantResponse);
        }

        [HttpGet("VerifyDOBByQRS")]
        public async Task<IActionResult> VerifyDOBByQRS(string token, string email, string userId, DateTime? date) {
            var applicantToken = await _applicantService.GetApplicantByTokenAsync(token, userId);
            if (applicantToken == null) {
                _applicantResponse.Status = ApplicantStatus.InvalidToken;
                return Ok(_applicantResponse);
            }
            var applicant = await _applicantService.GetApplicantByApplicantIdAsync(applicantToken.ApplicantId, userId);
            if (applicant == null) {
                _applicantResponse.Status = ApplicantStatus.Mismatch;
                return Ok(_applicantResponse);
            }
            var isDOBExists = await _userService.CheckDateOfBirthAsync(date, email);
            _applicantResponse.Status = Convert.ToDateTime(applicant.DOB) == date && !isDOBExists
                    ? ApplicantStatus.Valid : (isDOBExists ? ApplicantStatus.Exists : ApplicantStatus.Mismatch);
            _applicantResponse.Applicant = applicant;
            return Ok(_applicantResponse);
        }

        [HttpPost("Tracking/Marketing")]
        public async Task<IActionResult> Marketing(PurlParams purlParams, string userId) {
            if (string.IsNullOrEmpty(userId))
                return BadRequest(ApplicantStatus.InvalidUser);

            _purlResponse.ApplicantTracking = await _applicantService.SaveApplicantTrackingPurlAsync(purlParams, userId, BrandHelper.GetBrandType(), PurlType.Marketing);
            _purlResponse.Status = _purlResponse.ApplicantTracking is null ? ApplicantStatus.Failed : ApplicantStatus.Success;
            return Ok(_purlResponse);
        }

        [HttpPost("Tracking/AQE")]
        public async Task<IActionResult> AQE(PurlParams purlParams, string userId) {
            if (string.IsNullOrEmpty(userId))
                return BadRequest(ApplicantStatus.InvalidUser);

            if (!string.IsNullOrWhiteSpace(purlParams.Token))
                return Ok(ApplicantStatus.RegisterAQEApplicant);

            _purlResponse.ApplicantTracking = await _applicantService.SaveApplicantTrackingPurlAsync(purlParams, userId, BrandHelper.GetBrandType(), PurlType.AQE);
            _purlResponse.Status = _purlResponse.ApplicantTracking is null ? ApplicantStatus.Failed : ApplicantStatus.Success;
            return Ok(_purlResponse);
        }

        [HttpPost("Tracking")]
        public async Task<IActionResult> SaveApplicantTracking(PurlParams purlParams, string userId) {
            if (string.IsNullOrWhiteSpace(userId))
                return BadRequest(ApplicantStatus.InvalidUser);

            _purlResponse.ApplicantTracking = await _applicantService.SaveApplicantTrackingPurlAsync(purlParams, userId, BrandHelper.GetBrandType(), PurlType.Default);
            _purlResponse.Status = _purlResponse.ApplicantTracking is null ? ApplicantStatus.Failed : ApplicantStatus.Success;
            return Ok(_purlResponse);
        }
    }
}
