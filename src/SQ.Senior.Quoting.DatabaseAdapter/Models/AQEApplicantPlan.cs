using System;

namespace SQ.Senior.Quoting.DatabaseAdapter.Models {
    public class AQEApplicantPlan {
        public int ID { get; set; }
        public long AccountId { get; set; }
        public long ApplicantId { get; set; }
        public string ContractId { get; set; }
        public string PlanId { get; set; }
        public int EffectiveYear { get; set; }
        public string SegmentId { get; set; }
        public bool Selected { get; set; }
        public DateTime DateCreated { get; set; }
        public string DrxPlanId { get; set; }
        public string DrxSessionId { get; set; }
        public string UserId { get; set; }
        public bool? IsDelete { get; set; }
        public string PlanName { get; set; }
        public decimal? PlanAmount { get; set; }
        public string CarrierName { get; set; }
        public decimal? PrescriptionDeductible { get; set; }
        public decimal? PrescriptionPremium { get; set; }
        public decimal? EstimatedAnnualPrescriptionCost { get; set; }
        public decimal? EstimatedAnnualMedicalCost { get; set; }
        public decimal? MaximumOutOfPocketCost { get; set; }
        public decimal? MedicalDeductible { get; set; }
        public decimal? MedicalPremium { get; set; }
        public decimal? PlanRating { get; set; }
        public string PlanSubType { get; set; }
        public string PlanType { get; set; }
        public string LogoName { get; set; }
        public string PlanYear { get; set; }
        public string PlanGUID { get; set; }
        public long? QrsPlanId { get; set; }
    }
}
