namespace SQ.Senior.Clients.DrxServices.Models {

    public class Medigap {
        public string Id { get; set; }
        public string ContractId { get; set; }
        public string PlanId { get; set; }
        public string SegmentId { get; set; }
        public int PlanType { get; set; }
        public string PlanSubType { get; set; }
        public string PlanName { get; set; }
        public string CarrierName { get; set; }
        public decimal AnnualPlanPremium { get; set; }
        public decimal MedicalDeductible { get; set; }
        public decimal MedicalPremium { get; set; }
        public decimal AnnualCalculatedPlanPremium { get; set; }
    }
}
