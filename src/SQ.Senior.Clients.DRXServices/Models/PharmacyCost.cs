using Newtonsoft.Json;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SQ.Senior.Clients.DrxServices.Models {

    public class PharmacyCost {
     
        [JsonProperty(PropertyName = "DrugCosts")]
        public List<PrescriptionCost> PrescriptionsCost { get; set; }
        public bool IsPreferred { get; set; }
        public List<MonthlyCost> MonthlyCosts { get; set; }
        public int PharmacyType { get; set; }
        public string PharmacyId { get; set; }
    }
}
