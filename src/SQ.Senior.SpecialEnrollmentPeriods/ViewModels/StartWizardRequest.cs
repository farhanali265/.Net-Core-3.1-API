using System;

namespace SQ.Senior.SpecialEnrollmentPeriods.ViewModels {
    public class StartWizardRequest : WizardRequestBase {
        public Guid? UserKey { get; set; }
        public bool AutoEvaluate { get; set; } = true;
    }
}
