using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace SQ.Senior.Clients.DrxServices.Models {

    [SerializableAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true)]
    public class PlanFormularyStatus {
        public bool HasPriorAuthorization { get; set; }
        public bool HasQuantityLimit { get; set; }
        public bool HasStepTherapy { get; set; }
        public string LabelName { get; set; }
        public bool LimitedAccess { get; set; }
        public ulong NDC { get; set; }
        public decimal QuantityLimitAmount { get; set; }
        [XmlIgnoreAttribute()]
        public bool QuantityLimitAmountSpecified { get; set; }
        public ushort QuantityLimitDays { get; set; }
        [XmlIgnoreAttribute()]
        public bool QuantityLimitDaysSpecified { get; set; }
        public string QuantityLimitDescription { get; set; }
        public string TierDescription { get; set; }
        public byte TierNumber { get; set; }
    }
}
