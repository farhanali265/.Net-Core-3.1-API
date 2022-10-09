using System;

namespace SQ.Senior.SpecialEnrollmentPeriods.Extensions {
    public static class StringExtensions {
        public static bool In(this string source, params string[] options) {
            return options != null && Array.Exists(options, option => option == source);
        }
    }
}
