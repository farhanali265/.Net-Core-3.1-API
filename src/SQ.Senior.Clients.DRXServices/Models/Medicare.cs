using System.Collections.Generic;
using System.Xml.Serialization;

namespace SQ.Senior.Clients.DrxServices.Models {

    public class Medicare {
        private PlanPlanTier[] planTiersField;
        [System.Xml.Serialization.XmlArrayItemAttribute("PlanTier", IsNullable = false)]
        public PlanPlanTier[] PlanTiers {
            get {
                return this.planTiersField;
            }
            set {
                this.planTiersField = value;
            }
        }
        private PlanPlanExtendedSection[] extendedPlanDetailsField;
        [System.Xml.Serialization.XmlArrayItemAttribute("PlanExtendedSection", IsNullable = false)]
        public PlanPlanExtendedSection[] ExtendedPlanDetails {
            get {
                return this.extendedPlanDetailsField;
            }
            set {
                this.extendedPlanDetailsField = value;
            }
        }
        [XmlElement("ID")]
        public string Id { get; set; }

        [XmlElement("ContractID")]
        public string ContractId { get; set; }

        [XmlElement("PlanID")]
        public string PlanId { get; set; }

        [XmlElement("SegmentID")]
        public string SegmentId { get; set; }
        public int PlanType { get; set; }
        public string PlanSubType { get; set; }
        public string PlanName { get; set; }
        public string CarrierName { get; set; }
        public decimal AnnualPlanPremium { get; set; }
        public decimal MedicalDeductible { get; set; }
        public decimal MedicalPremium { get; set; }

        [XmlElement("LogoURL")]
        public string LogoUrl { get; set; }
        public List<Document> Documents { get; set; }

        [XmlElement("DrugDeductible")]
        public decimal PrescriptionDeductible { get; set; }
        [XmlElement("DrugPremium")]
        public decimal PrescriptionPremium { get; set; }
        [XmlElement("DrugRating")]
        public decimal PrescriptionRating { get; set; }
        [XmlElement("EstimatedAnnualDrugCost")]
        public decimal EstimatedAnnualPrescriptionCost { get; set; }
        public decimal MailOrderAnnualCost { get; set; }
        public decimal EstimatedAnnualMedicalCost { get; set; }
        public decimal InitialCoverageLimit { get; set; }
        public decimal MaximumOutOfPocketCost { get; set; }
        public decimal MedicalRating { get; set; }
        public List<PlanDataField> PlanDataFields { get; set; }
        public double PlanRating { get; set; }
        public string PlanYear { get; set; }
        public List<PharmacyCost> PharmacyCosts { get; set; }

        [XmlElement("PlanDrugCoverage")]
        public List<FormularyStatus> PlanPrescriptionCoverage { get; set; }
        public decimal CalcMonthlyTotalPremium {
            get {
                return PrescriptionPremium + MedicalPremium;
            }
        }
        public decimal CalcAnnualTotalCostMA {
            get {
                return ((PrescriptionPremium + MedicalPremium) * 12) + EstimatedAnnualPrescriptionCost + EstimatedAnnualMedicalCost;
            }
        }
        public decimal CalcAnnualDrugCost {
            get {
                return EstimatedAnnualPrescriptionCost - (CalcMonthlyTotalPremium * 12);
            }
        }
    }
}
