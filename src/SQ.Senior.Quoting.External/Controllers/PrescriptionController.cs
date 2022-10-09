using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SQ.Senior.Clients.DrxServices.Infrastructure.Services;
using SQ.Senior.Quoting.External.Services.IServices;
using SQ.Senior.Quoting.External.Services.ViewModels.Prescription;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQ.Senior.Quoting.External.Controllers {
    [Route("api/prescriptions")]
    public class PrescriptionController : BaseController {

        private readonly IDrxApiClient _drxApiClient;

        private readonly IPrescriptionService _prescriptionService;
        public PrescriptionController(IPrescriptionService prescriptionService, IDrxApiClient drxApiClient) {
            _prescriptionService = prescriptionService;
            _drxApiClient = drxApiClient;
        }

        /// <summary>
        /// Get prescriptions by userId 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("~/api/users/{userId}/prescriptions")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PrescriptionResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Prescriptions(string userId) {
            if (string.IsNullOrWhiteSpace(userId))
                return BadRequest();

            var prescriptions = await _prescriptionService.GetPrescriptionAsync(userId);
            return Ok(prescriptions);
        }

        /// <summary>
        /// Edit prescription by prescription,userId and/or packageName
        /// </summary>
        /// <param name="prescriptionRequest"></param>
        /// <param name="userId"></param>
        /// <param name="prescriptionId"></param>
        /// <returns></returns>
        [HttpPost("~/api/users/{userId}/prescriptions/{prescriptionId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PrescriptionResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditPrescription([FromBody] PrescriptionRequest prescriptionRequest, string userId, int prescriptionId) {
            if (prescriptionRequest is null || prescriptionId < 1 || string.IsNullOrWhiteSpace(userId))
                return BadRequest();

            var updatedPrescription = await _prescriptionService.EditPrescriptionAsync(prescriptionRequest, userId, prescriptionId);
            return Ok(updatedPrescription);
        }
        /// <summary>
        /// Save prescription by prescriptions,userId and/or packageName in each object
        /// </summary>
        /// <param name="prescriptionsRequest"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost("~/api/users/{userId}/prescriptions")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PrescriptionResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SavePrescription([FromBody] List<PrescriptionRequest> prescriptionsRequest, string userId) {
            if (!prescriptionsRequest.Any() || string.IsNullOrWhiteSpace(userId))
                return BadRequest();

            var savedPrescription = await _prescriptionService.SavePrescriptionAsync(prescriptionsRequest, userId);
            return Ok(savedPrescription);
        }

        [HttpDelete("~/api/users/{userId}/prescriptions/{prescriptionId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeletePrescription(int prescriptionId, string userId) {
            if (string.IsNullOrWhiteSpace(userId) || prescriptionId <= 0)
                return BadRequest();

            await _prescriptionService.DeletePrescriptionAsync(prescriptionId, userId);
            return NoContent();
        }

        /// <summary>
        /// Search prescriptions by prefix prescription name from drx api.
        /// </summary>
        /// <param name="drugName "></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Clients.DrxServices.Models.Prescription>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SearchPrescriptions([FromQuery]string drugName) {
            if (string.IsNullOrWhiteSpace(drugName))
                return BadRequest();

            var searchedPrescriptions = await _drxApiClient.SearchPrescriptionsAsync(drugName);
            return Ok(searchedPrescriptions);
        }
    }
}
