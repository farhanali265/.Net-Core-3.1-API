using System.Collections.Generic;
using System.Xml.Serialization;

namespace SQ.Senior.Clients.DrxServices.Models {
    #region ----- Medicare Advantage/Prescription Drug  -----

    [XmlRoot(ElementName = "Plans")]
    public class Plans {
        [XmlElement(ElementName = "MedicarePlans")]
        public List<PlansMedicarePlans> MedicarePlans { get; set; }

        [XmlElement(ElementName = "MedigapPlans")]
        public List<PlansMedigapPlans> MedigapPlans { get; set; }
        public string planRequestDRXApiResponce { get; set; }
    }

    #endregion

    #region ----- Medicare Advantage/Prescription Drug Details -----


    [XmlRoot(ElementName = "Plan")]
    public class MedicarePlanDetails : Medicare {

        [XmlElement(ElementName = "drugCostMonthly")]
        public MedicarePlanDetails DrugCostMonthly { get; set; }
        public string ViewPlanYear { get; set; }
    }

    #endregion
}
