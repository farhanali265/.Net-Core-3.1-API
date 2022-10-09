using Microsoft.AspNetCore.Mvc;
using SQ.Senior.SpecialEnrollmentPeriods.Exceptions;
using SQ.Senior.SpecialEnrollmentPeriods.Handlers;
using SQ.Senior.SpecialEnrollmentPeriods.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SQ.Senior.Quoting.External.Controllers {
    public class QuoteQualifierController : BaseController {
        private readonly IWizardHandler _wizardHandler;

        public QuoteQualifierController(IWizardHandler wizardHandler) {
            _wizardHandler = wizardHandler ?? throw new ArgumentNullException(nameof(wizardHandler));
        }

        [HttpGet("sep/workflows")]
        public async Task<ActionResult<IEnumerable<WorkflowDisplay>>> GetSpecialEnrollmentPeriodWorkflows() {
            var workflowDisplays = await _wizardHandler.GetWorkflowDisplaysAsync();
            return Ok(workflowDisplays);
        }

        [HttpGet("sep/start")]
        public async Task<ActionResult<WizardResponse>> StartSpecialEnrollmentPeriodWizard([FromQuery] StartWizardRequest startWizardRequest) {
            if (startWizardRequest == null) {
                return BadRequest();
            }

            try {
                return Ok(await _wizardHandler.StartWizardAsync(startWizardRequest));
            }
            catch (ApplicantNotFoundException ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("sep/steps/{stepId}")]
        public async Task<ActionResult<WizardResponse>> GetStepSpecialEnrollmentPeriodWizard([FromRoute] int stepId, [FromQuery] GetStepRequest getStepRequest) {
            if (stepId <= 0 || getStepRequest == null) {
                return BadRequest();
            }

            return Ok(await _wizardHandler.GetStepAsync(stepId, getStepRequest.ApplicantId, getStepRequest.AccountId));
        }


        [HttpPost("sep/process")]
        public async Task<ActionResult<WizardResponse>> ProcessSpecialEnrollmentPeriodWizard([FromBody] ProcessWizardRequest processWizardRequest) {
            if (processWizardRequest?.QuestionResponses == null) {
                return BadRequest();
            }

            try {
                return Ok(await _wizardHandler.ProcessWizardRequestAsync(processWizardRequest));
            }
            catch (ApplicantNotFoundException ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}
