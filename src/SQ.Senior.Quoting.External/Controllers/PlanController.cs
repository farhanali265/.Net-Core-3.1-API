using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SQ.Senior.Clients.DrxServices.Models;
using SQ.Senior.Quoting.External.Services.IServices;
using SQ.Senior.Quoting.External.Services.ViewModels.Plan;
using System.Threading.Tasks;

namespace SQ.Senior.Quoting.External.Controllers {
    [Route("api/plans")]
    public class PlanController : BaseController {

        private readonly IUserPlanService _userPlanService;
        public PlanController(IUserPlanService userPlanService) {
            _userPlanService = userPlanService;
        }
        /// <summary>
        /// Get Plans by userId, zip, fips
        /// </summary>
        /// <param name="getPlansRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Plans))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPlans(GetPlansRequest getPlansRequest) {

            if (getPlansRequest is null || string.IsNullOrWhiteSpace(getPlansRequest.UserId) || string.IsNullOrWhiteSpace(getPlansRequest.ZipCode)
                || string.IsNullOrWhiteSpace(getPlansRequest.FipsCode)) {
                return BadRequest();
            }
            var plans = await _userPlanService.GetDrxPlansAsync(getPlansRequest);
            return Ok(plans);

        }

        /// <summary>
        /// Get Plans by userId, zip, fips
        /// </summary>
        /// <param name="getPlansRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Plans))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPlanDetail(GetPlansRequest getPlansRequest) {

            if (getPlansRequest is null || string.IsNullOrWhiteSpace(getPlansRequest.UserId) || string.IsNullOrWhiteSpace(getPlansRequest.ZipCode)
                || string.IsNullOrWhiteSpace(getPlansRequest.FipsCode)) {
                return BadRequest();
            }
            var plans = await _userPlanService.GetDrxPlansAsync(getPlansRequest);
            return Ok(plans);

        }        
    }
}
