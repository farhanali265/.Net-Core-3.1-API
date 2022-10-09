namespace SQ.Senior.Clients.DrxServices.Models {

    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PlanPlanExtendedSection {

        private byte headerTypeField;
        private PlanPlanExtendedSectionPlanExtendedDetail[] sectionDetailsField;
        public byte HeaderType {
            get {
                return this.headerTypeField;
            }
            set {
                this.headerTypeField = value;
            }
        }

        [System.Xml.Serialization.XmlArrayItemAttribute("PlanExtendedDetail", IsNullable = false)]
        public PlanPlanExtendedSectionPlanExtendedDetail[] SectionDetails {
            get {
                return this.sectionDetailsField;
            }
            set {
                this.sectionDetailsField = value;
            }
        }
    }

    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PlanPlanExtendedSectionPlanExtendedDetail {

        private string descriptionField;
        private bool isEnrollLinkField;
        private bool isOutOfNetworkField;
        private string linkNameField;
        private string valueField;
        private bool isExternalLinkField;
        private ExtendedPlanDetailsTypes[] types;

        public string Description {
            get {
                return this.descriptionField;
            }
            set {
                this.descriptionField = value;
            }
        }
        public bool IsEnrollLink {
            get {
                return this.isEnrollLinkField;
            }
            set {
                this.isEnrollLinkField = value;
            }
        }
        public bool IsOutOfNetwork {
            get {
                return this.isOutOfNetworkField;
            }
            set {
                this.isOutOfNetworkField = value;
            }
        }
        public string LinkName {
            get {
                return this.linkNameField;
            }
            set {
                this.linkNameField = value;
            }
        }
        [System.Xml.Serialization.XmlArrayItemAttribute("Type", IsNullable = false)]
        public ExtendedPlanDetailsTypes[] Types {
            get {
                return this.types;
            }
            set {
                this.types = value;
            }
        }
        public string Value {
            get {
                return this.valueField;
            }
            set {
                this.valueField = value;
            }
        }

        public bool isExternalLink {
            get {
                return this.isExternalLinkField;
            }
            set {
                this.isExternalLinkField = value;
            }
        }
    }

    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ExtendedPlanDetailsTypes {
        private string name;

        public string Name {
            get {
                return this.name;
            }
            set {
                this.name = value;
            }
        }
    }

    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PlanPharmacyCostDrugCost {

        private decimal afterGapField;

        private decimal beforeGapField;

        private decimal deductibleField;

        private PlanPharmacyCostDrugCostDrugFootnotes drugFootnotesField;

        private decimal fullCostField;

        private decimal gapField;

        private string labelNameField;

        private decimal quantityField;


        public decimal AfterGap {
            get {
                return this.afterGapField;
            }
            set {
                this.afterGapField = value;
            }
        }

        public decimal BeforeGap {
            get {
                return this.beforeGapField;
            }
            set {
                this.beforeGapField = value;
            }
        }

        public decimal Deductible {
            get {
                return this.deductibleField;
            }
            set {
                this.deductibleField = value;
            }
        }

        public PlanPharmacyCostDrugCostDrugFootnotes DrugFootnotes {
            get {
                return this.drugFootnotesField;
            }
            set {
                this.drugFootnotesField = value;
            }
        }

        public decimal FullCost {
            get {
                return this.fullCostField;
            }
            set {
                this.fullCostField = value;
            }
        }

        public decimal Gap {
            get {
                return this.gapField;
            }
            set {
                this.gapField = value;
            }
        }

        public string LabelName {
            get {
                return this.labelNameField;
            }
            set {
                this.labelNameField = value;
            }
        }

        public decimal Quantity {
            get {
                return this.quantityField;
            }
            set {
                this.quantityField = value;
            }
        }
    }

    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PlanPharmacyCostDrugCostDrugFootnotes {

        private PlanPharmacyCostDrugCostDrugFootnotesDrugFootnote drugFootnoteField;
        public PlanPharmacyCostDrugCostDrugFootnotesDrugFootnote DrugFootnote {
            get {
                return this.drugFootnoteField;
            }
            set {
                this.drugFootnoteField = value;
            }
        }
    }

    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PlanPharmacyCostDrugCostDrugFootnotesDrugFootnote {

        private byte numberField;


        public byte Number {
            get {
                return this.numberField;
            }
            set {
                this.numberField = value;
            }
        }
    }

    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PlanPharmacyCostMonthlyCost {

        private PlanPharmacyCostMonthlyCostMonthlyDrugCostDetail[] costDetailField;

        private string costPhasesField;

        private byte monthIdField;

        private decimal totalMonthlyCostField;


        [System.Xml.Serialization.XmlArrayItemAttribute("MonthlyDrugCostDetail", IsNullable = false)]
        public PlanPharmacyCostMonthlyCostMonthlyDrugCostDetail[] CostDetail {
            get {
                return this.costDetailField;
            }
            set {
                this.costDetailField = value;
            }
        }


        public string CostPhases {
            get {
                return this.costPhasesField;
            }
            set {
                this.costPhasesField = value;
            }
        }

        [System.Xml.Serialization.XmlArrayItemAttribute("MonthID")]
        public byte MonthId {
            get {
                return this.monthIdField;
            }
            set {
                this.monthIdField = value;
            }
        }

        public decimal TotalMonthlyCost {
            get {
                return this.totalMonthlyCostField;
            }
            set {
                this.totalMonthlyCostField = value;
            }
        }
    }

    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PlanPharmacyCostMonthlyCostMonthlyDrugCostDetail {

        private decimal fullCostField;

        private string labelNameField;

        private decimal memberCostField;

        private string phaseField;
        public decimal FullCost {
            get {
                return this.fullCostField;
            }
            set {
                this.fullCostField = value;
            }
        }

        public string LabelName {
            get {
                return this.labelNameField;
            }
            set {
                this.labelNameField = value;
            }
        }
        public decimal MemberCost {
            get {
                return this.memberCostField;
            }
            set {
                this.memberCostField = value;
            }
        }
        public string Phase {
            get {
                return this.phaseField;
            }
            set {
                this.phaseField = value;
            }
        }
    }


    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PlanPlanDataField {

        private string descriptionField;

        private string nameField;

        private string typeField;


        public string Description {
            get {
                return this.descriptionField;
            }
            set {
                this.descriptionField = value;
            }
        }


        public string Name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }


        public string Type {
            get {
                return this.typeField;
            }
            set {
                this.typeField = value;
            }
        }
    }


    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PlanPlanTier {

        private byte daysSupplyField;

        private byte pharmacyTypeField;

        private PlanPlanTierTier[] tierInformationField;


        public byte DaysSupply {
            get {
                return this.daysSupplyField;
            }
            set {
                this.daysSupplyField = value;
            }
        }


        public byte PharmacyType {
            get {
                return this.pharmacyTypeField;
            }
            set {
                this.pharmacyTypeField = value;
            }
        }


        [System.Xml.Serialization.XmlArrayItemAttribute("Tier", IsNullable = false)]
        public PlanPlanTierTier[] TierInformation {
            get {
                return this.tierInformationField;
            }
            set {
                this.tierInformationField = value;
            }
        }
    }


    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PlanPlanTierTier {

        private string amountField;

        private string descriptionField;

        private byte tierNumberField;

        public string Amount {
            get {
                return this.amountField;
            }
            set {
                this.amountField = value;
            }
        }


        public string Description {
            get {
                return this.descriptionField;
            }
            set {
                this.descriptionField = value;
            }
        }


        public byte TierNumber {
            get {
                return this.tierNumberField;
            }
            set {
                this.tierNumberField = value;
            }
        }
    }
}
