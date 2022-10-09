using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SQ.Senior.Clients.QrsService.Models;
using SQ.Senior.Quoting.External.Helpers;
using SQ.Senior.Quoting.External.Services.IServices;
using SQ.Senior.Quoting.External.Services.ViewModels.Plan;
using SQ.Senior.Quoting.External.Services.ViewModels.Providers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SQ.Senior.Quoting.External.Controllers {

    [Route("api/providers")]
    public class ProviderController : BaseController {
        private readonly IProviderService _providerService;

        public ProviderController(IProviderService providerService) {
            _providerService = providerService;
        }

        /// <summary>
        /// Get providers with userId and activeprovider bit
        /// </summary>
        /// <param name="userId"></param>   
        /// <param name="isActive"></param>
        /// <returns></returns>
        [HttpGet("~/api/users/{userId}/providers")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProviderResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllProviders([FromQuery] bool isActive, string userId) {

            if (string.IsNullOrWhiteSpace(userId)) {
                return BadRequest();
            }
            var providers = await _providerService.GetApplicantProviderAsync(userId, isActive);
            return Ok(providers);
        }

        /// <summary>
        /// Searches NPI registry with the provided request
        /// </summary>
        /// <param name="searchProvidersRequest"></param>
        /// <returns></returns>
        [HttpPost("search")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<QRSSearchProvider>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetNpiResults([FromBody] SearchProvidersRequest searchProvidersRequest) {
            if (searchProvidersRequest is null) {
                return BadRequest();
            }

            var providers = await _providerService.SearchProviders(searchProvidersRequest);
            return Ok(providers);
        }

        /// <summary>
        /// Save Provider by national provider identifier and userId
        /// </summary>
        /// <param name="addProviderRequest"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost("~/api/users/{userId}/providers/")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProviderResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddProvider([FromBody] AddProviderRequest addProviderRequest, string userId) {

            if (addProviderRequest is null || addProviderRequest.NationalProviderIdentifier < 1 || string.IsNullOrWhiteSpace(userId)) {
                return BadRequest();
            }
            _ = await _providerService.SaveProvidersAsync(addProviderRequest.NationalProviderIdentifier, userId, BrandHelper.GetBrandType());
            var applicantProviders = await _providerService.GetApplicantProviderAsync(userId, true);
            return Ok(applicantProviders);
        }

        /// <summary>
        /// Delete provider with userId and providerId
        /// </summary>
        /// <param name="userId"></param>   
        /// <param name="providerId"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("~/api/users/{userId}/providerid/{providerId}")]
        public async Task<IActionResult> DeleteProvider(string userId, int providerId) {

            if (providerId < 1 || string.IsNullOrWhiteSpace(userId)) {
                return BadRequest();
            }
            await _providerService.DeleteProviderAsync(userId, providerId);
            return NoContent();
        }
    }
}
