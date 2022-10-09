using System;

namespace SQ.Senior.Clients.DrxServices.Models {
    public class ApplicantCompleteInfo {
        public Int64 ApplicantId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string BirthDate { get; set; }
        public int Age { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public int State { get; set; }
        public string ZipCode { get; set; }
        public string County { get; set; }
        public string FIPSCode { get; set; }
        public string EmailAddress { get; set; }
        public DateTime? PartBEffectiveDate { get; set; }
        public string StateAbbr { get; set; }
        public int HealthStatus { get; set; }
        public int TobaccoUse { get; set; }
        public int Disabled { get; set; }
        public DateTime? DisabilityDate { get; set; }
        public string HomePhone { get; set; }
        public string MobilePhone { get; set; }
        public Int64 SQSApplicantId { get; set; }
        public Int64 SQSAccountId { get; set; }
        public DateTime? MedSuppEffectiveDate { get; set; }
        public bool CustomerType { get; set; }
        public string GuaranteedIssue { get; set; }
        public string OpenEnrollment { get; set; }
        public string MaritalStatus { get; set; }
        public string CurrentCoverage { get; set; }
        public int? SubsidyLevel { get; set; }
        public int SubsidyPercent { get; set; }
        public bool IsPrimary { get; set; }
    }
}
