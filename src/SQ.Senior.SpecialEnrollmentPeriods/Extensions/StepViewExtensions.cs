using SQ.Senior.SpecialEnrollmentPeriods.ViewModels;
using System.Linq;

namespace SQ.Senior.SpecialEnrollmentPeriods.Extensions {
    public static class StepViewExtensions {
        public static bool AllQuestionsHaveMappedValues(this StepView stepView) {
            return stepView != null && 
                stepView.Questions.All(question => !string.IsNullOrWhiteSpace(question.MappedValue));
        }
    }
}
