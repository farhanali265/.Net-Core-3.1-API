using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace SQ.Senior.Clients.DrxServices.Models {

    [SerializableAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true)]
    public class PlanPharmacyCostMonthly {
        [XmlArrayItemAttribute("MonthlyDrugCostDetail", IsNullable = false)]
        public PlanPharmacyMonthlyCostDetail[] CostDetail { get; set; }
        public string CostPhases { get; set; }
        public byte MonthId { get; set; }
        public decimal TotalMonthlyCost { get; set; }
    }
}
