using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SQ.Senior.Quoting.External.Services.IServices;
using SQ.Senior.Quoting.External.Services.ViewModels.Fips;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SQ.Senior.Quoting.External.Controllers {
    [Route("api/fips")]
    public class FipsController : BaseController {
        private readonly IZipFipsMappingService _zipFipsMappingService;
        
        public FipsController(IZipFipsMappingService zipFipsMappingService) {
            _zipFipsMappingService = zipFipsMappingService;
        }

        /// <summary>
        /// Returns a list of FIPS mappings 
        /// </summary>
        /// <param name="zipCode"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<FipsMappingResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetFipsAsync([FromQuery, BindRequired] string zipCode) {
            if (string.IsNullOrWhiteSpace(zipCode)) {
                return BadRequest();
            }

            var fipsMappings = await _zipFipsMappingService.GetFipsMappingResponsesAsync(zipCode);
            return Ok(fipsMappings);
        }
    }
}
