using System;

namespace SQ.Senior.Quoting.DatabaseAdapter.Models {
    public class DrxEnrollmentLog {
        public int ID { get; set; }
        public long SQSAccountId { get; set; }
        public long SQSApplicantId { get; set; }
        public string UserID { get; set; }
        public string PlanName { get; set; }
        public string Provider { get; set; }
        public string ContractId { get; set; }
        public string PbpId { get; set; }
        public string SegmentId { get; set; }
        public string DrxPlanId { get; set; }
        public string DrxSessionId { get; set; }
        public int? EmfluenceEmailLogId { get; set; }
        public string RequestXml { get; set; }
        public string ResponseXml { get; set; }
        public string EnrollmentError { get; set; }
        public DateTime DateCreated { get; set; }
        public string PhysicianId { get; set; }
        public string PhoneNumber { get; set; }
    }
}