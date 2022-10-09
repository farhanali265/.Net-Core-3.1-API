using System;
using System.Collections.Generic;

namespace SQ.Senior.SpecialEnrollmentPeriods.ViewModels {
    public class ProcessWizardRequest : WizardRequestBase {
        public int WorkflowId { get; set; }
        public int StepId { get; set; }
        public Guid? UserKey { get; set; }
        public bool AutoEvaluate { get; set; } = true;
        public IEnumerable<QuestionResponse> QuestionResponses { get; set; }
    }
}
