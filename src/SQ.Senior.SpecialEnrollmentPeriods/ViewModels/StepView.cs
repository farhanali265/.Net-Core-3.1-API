using System.Collections.Generic;

namespace SQ.Senior.SpecialEnrollmentPeriods.ViewModels {
    public class StepView {
        public int WorkflowId { get; set; }
        public int StepId { get; set; }
        public string DisplayText { get; set; }
        public IEnumerable<QuestionView> Questions { get; set; }
    }
}
