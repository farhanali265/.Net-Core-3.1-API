using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace SQ.Senior.Clients.DrxServices.Models {

    [SerializableAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true)]
    public class PlanRidersTypeRider {
        public decimal ApplicationFee { get; set; }
        public PlanRiderTypeExtendedSection ExtendedPlanDetails { get; set; }
        public bool HasCalculatedMedicalPremium { get; set; }
        public decimal MedicalPremium { get; set; }
        public string PlanId { get; set; }
        public string PlanName { get; set; }
        public string PlanSubType { get; set; }
        public string PlanType { get; set; }
        public string PlanTypeAbbreviation { get; set; }
        public string PlanTypeDescription { get; set; }
        public object ValidAdditionalRiderIDs { get; set; }
    }
}

