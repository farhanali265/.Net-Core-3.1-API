using System;

namespace SQ.Senior.Quoting.DatabaseAdapter.Models {
    public class ApplicantPrescription {
        public long ID { get; set; }
        public Guid ApplicantID { get; set; }
        public int PrescriptionID { get; set; }
        public int AmountPer30Days { get; set; }
        public string DosageID { get; set; }
        public string Package { get; set; }
        public DateTime Dated { get; set; }
        public string SelectedPrescriptionInfoAsXml { get; set; }
        public string UserId { get; set; }
        public string PrescriptionName { get; set; }
    }
}