namespace SQ.Senior.Clients.DrxServices.Models {

    [System.Serializable()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class Plan {

        private byte annualPlanPremiumField;
        private string carrierNameField;
        private ushort catastrophicLimitField;
        private string contractIdField;
        private byte contributionTypeField;
        private PlanDocument[] documentsField;
        private byte drugDeductibleField;
        private byte drugPremiumField;
        private decimal estimatedAnnualDrugCostField;
        private decimal estimatedAnnualDrugCostPartialYearField;
        private decimal estimatedAnnualMailOrderDrugCostPartialYearField;
        private decimal estimatedAnnualMedicalCostField;
        private decimal estimatedAnnualMedicalCostPartialYearField;
        private PlanPlanExtendedSection[] extendedPlanDetailsField;
        private byte externalSourceDescriptionField;
        private string externalSourceIdField;
        private ushort formularyExternalIdField;
        private PlanFormularyTier[] formularyTiersField;
        private string formularyVersionField;
        private bool hasDrugCoverageField;
        private bool hasMailDrugBenefitsField;
        private bool hasPreferredMailPharmacyNetworkField;
        private bool hasPreferredPharmacyNetworkField;
        private bool hasPreferredRetailPharmacyNetworkField;
        private string idField;
        private ushort initialCoverageLimitField;
        private string logoUrlField;
        private decimal mailOrderAnnualCostField;
        private ushort maximumOutOfPocketCostField;
        private byte medicalDeductibleField;
        private byte medicalPremiumField;
        private PlanPharmacyCost[] pharmacyCostsField;
        private PlanPlanDataField[] planDataFieldsField;
        private PlanFormularyStatus[] planDrugCoverageField;
        private byte planIdField;
        private string planNameField;
        private byte planRatingField;
        private string planSubTypeField;
        private PlanPlanTier[] planTiersField;
        private byte planTypeField;
        private ushort planYearField;
        private string providerURLField;
        private byte segmentIdField;
        public byte AnnualPlanPremium {
            get {
                return this.annualPlanPremiumField;
            }
            set {
                this.annualPlanPremiumField = value;
            }
        }
        public string CarrierName {
            get {
                return this.carrierNameField;
            }
            set {
                this.carrierNameField = value;
            }
        }
        public ushort CatastrophicLimit {
            get {
                return this.catastrophicLimitField;
            }
            set {
                this.catastrophicLimitField = value;
            }
        }

        [System.Xml.Serialization.XmlArrayItemAttribute("ContractID")]
        public string ContractId {
            get {
                return this.contractIdField;
            }
            set {
                this.contractIdField = value;
            }
        }
        public byte ContributionType {
            get {
                return this.contributionTypeField;
            }
            set {
                this.contributionTypeField = value;
            }
        }
        [System.Xml.Serialization.XmlArrayItemAttribute("Document", IsNullable = false)]
        public PlanDocument[] Documents {
            get {
                return this.documentsField;
            }
            set {
                this.documentsField = value;
            }
        }
        public byte DrugDeductible {
            get {
                return this.drugDeductibleField;
            }
            set {
                this.drugDeductibleField = value;
            }
        }
        public byte DrugPremium {
            get {
                return this.drugPremiumField;
            }
            set {
                this.drugPremiumField = value;
            }
        }
        public decimal EstimatedAnnualDrugCost {
            get {
                return this.estimatedAnnualDrugCostField;
            }
            set {
                this.estimatedAnnualDrugCostField = value;
            }
        }
        public decimal EstimatedAnnualDrugCostPartialYear {
            get {
                return this.estimatedAnnualDrugCostPartialYearField;
            }
            set {
                this.estimatedAnnualDrugCostPartialYearField = value;
            }
        }
        public decimal EstimatedAnnualMailOrderDrugCostPartialYear {
            get {
                return this.estimatedAnnualMailOrderDrugCostPartialYearField;
            }
            set {
                this.estimatedAnnualMailOrderDrugCostPartialYearField = value;
            }
        }
        public decimal EstimatedAnnualMedicalCost {
            get {
                return this.estimatedAnnualMedicalCostField;
            }
            set {
                this.estimatedAnnualMedicalCostField = value;
            }
        }
        public decimal EstimatedAnnualMedicalCostPartialYear {
            get {
                return this.estimatedAnnualMedicalCostPartialYearField;
            }
            set {
                this.estimatedAnnualMedicalCostPartialYearField = value;
            }
        }

        [System.Xml.Serialization.XmlArrayItemAttribute("PlanExtendedSection", IsNullable = false)]
        public PlanPlanExtendedSection[] ExtendedPlanDetails {
            get {
                return this.extendedPlanDetailsField;
            }
            set {
                this.extendedPlanDetailsField = value;
            }
        }
        public byte ExternalSourceDescription {
            get {
                return this.externalSourceDescriptionField;
            }
            set {
                this.externalSourceDescriptionField = value;
            }
        }

        [System.Xml.Serialization.XmlArrayItemAttribute("ExternalSourceID")]
        public string ExternalSourceId {
            get {
                return this.externalSourceIdField;
            }
            set {
                this.externalSourceIdField = value;
            }
        }

        [System.Xml.Serialization.XmlArrayItemAttribute("FormularyExternalID")]
        public ushort FormularyExternalId {
            get {
                return this.formularyExternalIdField;
            }
            set {
                this.formularyExternalIdField = value;
            }
        }

        [System.Xml.Serialization.XmlArrayItemAttribute("PlanFormularyTier", Namespace = "http://schemas.datacontract.org/2004/07/CoreServices.PlanInfo", IsNullable = false)]
        public PlanFormularyTier[] FormularyTiers {
            get {
                return this.formularyTiersField;
            }
            set {
                this.formularyTiersField = value;
            }
        }
        public string FormularyVersion {
            get {
                return this.formularyVersionField;
            }
            set {
                this.formularyVersionField = value;
            }
        }
        public bool HasDrugCoverage {
            get {
                return this.hasDrugCoverageField;
            }
            set {
                this.hasDrugCoverageField = value;
            }
        }
        public bool HasMailDrugBenefits {
            get {
                return this.hasMailDrugBenefitsField;
            }
            set {
                this.hasMailDrugBenefitsField = value;
            }
        }
        public bool HasPreferredMailPharmacyNetwork {
            get {
                return this.hasPreferredMailPharmacyNetworkField;
            }
            set {
                this.hasPreferredMailPharmacyNetworkField = value;
            }
        }
        public bool HasPreferredPharmacyNetwork {
            get {
                return this.hasPreferredPharmacyNetworkField;
            }
            set {
                this.hasPreferredPharmacyNetworkField = value;
            }
        }
        public bool HasPreferredRetailPharmacyNetwork {
            get {
                return this.hasPreferredRetailPharmacyNetworkField;
            }
            set {
                this.hasPreferredRetailPharmacyNetworkField = value;
            }
        }

        [System.Xml.Serialization.XmlArrayItemAttribute("ID")]
        public string Id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
        public ushort InitialCoverageLimit {
            get {
                return this.initialCoverageLimitField;
            }
            set {
                this.initialCoverageLimitField = value;
            }
        }
        public string LogoURL {
            get {
                return this.logoUrlField;
            }
            set {
                this.logoUrlField = value;
            }
        }
        public decimal MailOrderAnnualCost {
            get {
                return this.mailOrderAnnualCostField;
            }
            set {
                this.mailOrderAnnualCostField = value;
            }
        }
        public ushort MaximumOutOfPocketCost {
            get {
                return this.maximumOutOfPocketCostField;
            }
            set {
                this.maximumOutOfPocketCostField = value;
            }
        }
        public byte MedicalDeductible {
            get {
                return this.medicalDeductibleField;
            }
            set {
                this.medicalDeductibleField = value;
            }
        }
        public byte MedicalPremium {
            get {
                return this.medicalPremiumField;
            }
            set {
                this.medicalPremiumField = value;
            }
        }
        [System.Xml.Serialization.XmlArrayItemAttribute("PharmacyCost", IsNullable = false)]
        public PlanPharmacyCost[] PharmacyCosts {
            get {
                return this.pharmacyCostsField;
            }
            set {
                this.pharmacyCostsField = value;
            }
        }
        [System.Xml.Serialization.XmlArrayItemAttribute("PlanDataField", IsNullable = false)]
        public PlanPlanDataField[] PlanDataFields {
            get {
                return this.planDataFieldsField;
            }
            set {
                this.planDataFieldsField = value;
            }
        }
        [System.Xml.Serialization.XmlArrayItemAttribute("FormularyStatus", IsNullable = false)]
        public PlanFormularyStatus[] PlanDrugCoverage {
            get {
                return this.planDrugCoverageField;
            }
            set {
                this.planDrugCoverageField = value;
            }
        }

        [System.Xml.Serialization.XmlArrayItemAttribute("PlanID")]
        public byte PlanId {
            get {
                return this.planIdField;
            }
            set {
                this.planIdField = value;
            }
        }

        public string PlanName {
            get {
                return this.planNameField;
            }
            set {
                this.planNameField = value;
            }
        }
        public byte PlanRating {
            get {
                return this.planRatingField;
            }
            set {
                this.planRatingField = value;
            }
        }

        public string PlanSubType {
            get {
                return this.planSubTypeField;
            }
            set {
                this.planSubTypeField = value;
            }
        }

        [System.Xml.Serialization.XmlArrayItemAttribute("PlanTier", IsNullable = false)]
        public PlanPlanTier[] PlanTiers {
            get {
                return this.planTiersField;
            }
            set {
                this.planTiersField = value;
            }
        }

        public byte PlanType {
            get {
                return this.planTypeField;
            }
            set {
                this.planTypeField = value;
            }
        }

        public ushort PlanYear {
            get {
                return this.planYearField;
            }
            set {
                this.planYearField = value;
            }
        }

        public string ProviderURL {
            get {
                return this.providerURLField;
            }
            set {
                this.providerURLField = value;
            }
        }

        [System.Xml.Serialization.XmlArrayItemAttribute("SegmentID")]
        public byte SegmentId {
            get {
                return this.segmentIdField;
            }
            set {
                this.segmentIdField = value;
            }
        }
    }
}
