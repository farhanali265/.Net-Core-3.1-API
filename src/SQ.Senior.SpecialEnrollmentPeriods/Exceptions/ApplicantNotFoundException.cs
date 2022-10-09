using System;

namespace SQ.Senior.SpecialEnrollmentPeriods.Exceptions {
    public class ApplicantNotFoundException : Exception {
        public ApplicantNotFoundException() : base("Applicant not found.") { }
    }
}
