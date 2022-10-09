using System;

namespace SQ.Senior.Quoting.DatabaseAdapter.Models {
    public class UserPlan {
        public Guid UserPlainID { get; set; }
        public string UserID { get; set; }
        public string CarrierName { get; set; }
        public string ContractID { get; set; }
        public decimal? PrescriptionDeductible { get; set; }
        public decimal? PrescriptionPremium { get; set; }
        public decimal? EstimatedAnnualPrescriptionCost { get; set; }
        public decimal? EstimatedAnnualMedicalCost { get; set; }
        public string ID { get; set; }
        public decimal? MaximumOutOfPocketCost { get; set; }
        public decimal? MedicalDeductible { get; set; }
        public decimal? MedicalPremium { get; set; }
        public string PlanID { get; set; }
        public string PlanName { get; set; }
        public decimal? PlanRating { get; set; }
        public string PlanSubType { get; set; }
        public string PlanType { get; set; }
        public string PlanYear { get; set; }
        public string SegmentID { get; set; }
        public DateTime? AddDate { get; set; }
        public string LogoName { get; set; }
    }
}