using SQ.Senior.SpecialEnrollmentPeriods.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SQ.Senior.SpecialEnrollmentPeriods.Handlers {
    public interface IWizardHandler {
        Task<IEnumerable<WorkflowDisplay>> GetWorkflowDisplaysAsync();
        Task<WizardResponse> GetStepAsync(int stepId, int applicantId, int accountId);
        Task<WizardResponse> StartWizardAsync(StartWizardRequest startWizardRequest);
        Task<WizardResponse> ProcessWizardRequestAsync(ProcessWizardRequest processWizardRequest);
    }
}
