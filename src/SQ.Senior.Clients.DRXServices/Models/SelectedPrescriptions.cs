namespace SQ.Senior.Clients.DrxServices.Models {

    public class SelectedPrescriptions {
        public SelectedPrescriptions() {
            SelectedPrescriptionInfo = new PrescriptionInfo();
        }
        public long Id { get; set; }
        public bool Confirmed { get; set; }
        public int AmountPerThirtyDays { get; set; }
        public string DosageId { get; set; }
        public string Package { get; set; }
        public PrescriptionInfo SelectedPrescriptionInfo { get; set; }
        public string SelectedPrescriptionInfoAsXml { get; set; }
        public string NationalDrugCode { get; set; }
        public string PrescriptionName { get; set; }
        public string MostLikelyCondition { get; set; }
    }
}
