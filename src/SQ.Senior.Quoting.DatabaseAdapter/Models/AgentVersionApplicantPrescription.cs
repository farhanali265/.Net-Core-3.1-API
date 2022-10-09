using System;

namespace SQ.Senior.Quoting.DatabaseAdapter.Models {

    public class AgentVersionApplicantPrescription
    {
        public long Id { get; set; }
        public long ApplicantId { get; set; }
        public int PrescriptionId { get; set; }
        public int AmountPerThirtyDays { get; set; }
        public string DosageId { get; set; }
        public string Package { get; set; }
        public DateTime Dated { get; set; }
        public string SelectedPrescriptionInfoAsXml { get; set; }
        public long SqsApplicantId { get; set; }
        public long SqsAccountId { get; set; }
        public string NationalDrugCode { get; set; }
        public string PrescriptionName { get; set; }
        public string UserId { get; set; }
        public string PrescriptionLabel { get; set; }
        public string PackageName { get; set; }
        public long? QrsAgentPrescriptionId { get; set; }
    }
}
