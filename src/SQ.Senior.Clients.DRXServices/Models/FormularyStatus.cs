namespace SQ.Senior.Clients.DrxServices.Models {

    public class FormularyStatus {
        public bool HasPriorAuthorization { get; set; }
        public bool HasQuantityLimit { get; set; }
        public bool HasStepTherapy { get; set; }
        public string LabelName { get; set; }
        public bool LimitedAccess { get; set; }
        public string NDC { get; set; }
        public float QuantityLimitAmount { get; set; }
        public int QuantityLimitDays { get; set; }
        public string QuantityLimitDescription { get; set; }
        public string TierDescription { get; set; }
        public int TierNumber { get; set; }
    }
}
