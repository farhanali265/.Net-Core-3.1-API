namespace SQ.Senior.Quoting.External.Services.ViewModels.Prescription {
    public class PrescriptionResponse {
        public long Id { get; set; }
        public int PrescriptionId { get; set; }
        public int AmountPerThirtyDays { get; set; }
        public string DosageId { get; set; }
        public string Package { get; set; }
        public string SelectedPrescriptionInfoAsXml { get; set; }
        public string PrescriptionName { get; set; }
        public string PrescriptionLabel { get; set; }
        public string PackageName { get; set; }
        public string NationalDrugCode { get; set; }
    }
}
