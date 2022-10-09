namespace SQ.Senior.Clients.DrxServices.Models {

    public class CopayPrices {
        public decimal Cost { get; set; }
        public int CostType { get; set; }
        public bool IsMailOrder { get; set; }
        public bool IsPreferredPharmacy { get; set; }
        public int DaysOfSupply { get; set; }
        public bool DefaultBenefit { get; set; }
    }
}
