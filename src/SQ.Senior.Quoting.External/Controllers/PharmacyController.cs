using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SQ.Senior.Clients.DrxServices.Infrastructure.Services;
using SQ.Senior.Quoting.External.Infrastructure.Constants;
using SQ.Senior.Quoting.External.Services.IServices;
using SQ.Senior.Quoting.External.Services.ViewModels.Pharmacy;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SQ.Senior.Quoting.External.Controllers {
    [Route("api/pharmacies")]
    public class PharmacyController : BaseController {
        private readonly IPharmacyService _pharmacyService;
        private readonly IAuthentication _drxAuthentication;
        private readonly IDrxApiClient _drxApiClient;

        public PharmacyController(IPharmacyService pharmacyService, IAuthentication drxAuthentication, IDrxApiClient drxApiClient) {
            _pharmacyService = pharmacyService;
            _drxAuthentication = drxAuthentication;
            _drxApiClient = drxApiClient;
        }

        /// <summary>
        /// Save pharmacy by pharmcy search properties
        /// </summary>
        /// <param name="savePharmacyRequest"></param>
        /// <returns></returns>
        [HttpPost("~/api/users/{userId}/pharmacies/")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PharmacyResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SavePharmacy([FromBody] SavePharmacyRequest savePharmacyRequest, string userId) {

            if (savePharmacyRequest is null || string.IsNullOrWhiteSpace(userId))
                return BadRequest(PharmacyStatus.Failed);

            var savedPharmacy = await _pharmacyService.SavePharmacyAsync(userId, savePharmacyRequest);
            return Ok(savedPharmacy);
        }

        /// <summary>
        /// Delete pharmacy by pharmacyId and userId
        /// </summary>
        /// <param name="pharmacyId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("~/api/users/{userId}/pharmacies/{pharmacyId}")]
        public async Task<IActionResult> DeletePharmacy(string pharmacyId, string userId) {

            if (string.IsNullOrWhiteSpace(pharmacyId) || string.IsNullOrWhiteSpace(userId))
                return BadRequest(PharmacyStatus.Failed);

            var isPharmacyDeleted = await _pharmacyService.DeletePharmacyAsync(userId, pharmacyId);

            if (isPharmacyDeleted)
                await _pharmacyService.UpdateUserPharmacyStatusAsync(userId);

            return NoContent();
        }

        /// <summary>
        /// Get pharmacy by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PharmacyResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("~/api/users/{userId}/pharmacies")]
        public async Task<IActionResult> GetPharmacy(string userId) {

            if (string.IsNullOrWhiteSpace(userId))
                return BadRequest(PharmacyStatus.Failed);

            var pharmacy = await _pharmacyService.GetPharmacyAsync(userId);
            return Ok(pharmacy);
        }

        /// <summary>
        /// Searches pharmacies by zipcode, name and/or distance
        /// </summary>
        /// <param name="searchRequest"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Clients.DrxServices.Models.Pharmacy>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SearchPharmacies([FromQuery] SearchPharmaciesRequest searchRequest) {

            if (searchRequest != null && string.IsNullOrWhiteSpace(searchRequest.ZipCode))
                return BadRequest(PharmacyStatus.Failed);

            var drxTokenObject = await _drxAuthentication.GetDrxTokenAsync();
            if (drxTokenObject is null)
                return BadRequest(DrxRequestStatus.InvalidDrxToken);

            var pharmacies = await _drxApiClient.SearchDrxPharmaciesAsync(searchRequest.ZipCode, drxTokenObject.AccessToken, searchRequest.Distance, searchRequest.Name);
            return Ok(pharmacies);
        }
    }
}
