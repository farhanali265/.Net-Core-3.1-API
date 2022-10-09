using System;

namespace SQ.Senior.Quoting.Internal.DatabaseAdapter.Models {
    public class AgentVersionApplicant {
        public long ApplicantId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int? StateId { get; set; }
        public string ZipCode { get; set; }
        public string FIPSCode { get; set; }
        public string County { get; set; }
        public string EmailAddress { get; set; }
        public string HomePhone { get; set; }
        public string MobilePhone { get; set; }
        public bool? Disabled { get; set; }
        public DateTime? DateDisabled { get; set; }
        public bool? TobaccoUse { get; set; }
        public int? HealthStatus { get; set; }
        public DateTime? PartBEffectiveDate { get; set; }
        public DateTime? PartAEffectiveDate { get; set; }
        public long? SQSApplicantId { get; set; }
        public long? SQSAccountId { get; set; }
        public DateTime? MedSuppEffectiveDate { get; set; }
        public string GuaranteedIssue { get; set; }
        public string OpenEnrollment { get; set; }
        public string MaritalStatus { get; set; }
        public int? SubsidyLevel { get; set; }
        public int? SubsidyPercent { get; set; }
        public bool IsPrimary { get; set; }
        public string CurrentCoverage { get; set; }
        public string Address2 { get; set; }
        public bool? ESRD { get; set; }
        public DateTime? FirstQuoteDate { get; set; }
        public bool? MAPermissions { get; set; }
        public bool? MAPDDisclosure { get; set; }
        public long? QRSApplicantId { get; set; }
        public bool? IsOutboundEnrollment { get; set; }
        public int? ExtraHelpLevel { get; set; }
        public int? MedicaidLevel { get; set; }
        public int? CampaignID { get; set; }
        public string MBI { get; set; }
    }
}
