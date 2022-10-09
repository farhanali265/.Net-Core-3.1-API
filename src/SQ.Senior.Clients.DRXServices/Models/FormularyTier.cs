using System.Collections.Generic;

namespace SQ.Senior.Clients.DrxServices.Models {

    public class FormularyTier {
        public List<FomularyObject> FormularyTiers { get; set; }
        //TODO: need to rename with team collaboration
        public PlanFormularyTier[] FormulayTiers { get; set; }
        public string TierDescription { get; set; }
        public double Radius { get; set; }
        public string CarrierName { get; set; }
    }
}
