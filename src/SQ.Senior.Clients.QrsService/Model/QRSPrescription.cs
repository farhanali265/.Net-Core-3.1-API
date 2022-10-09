using Newtonsoft.Json;
using System;

namespace SQ.Senior.Clients.QrsService.Models {
    public class QRSPrescription : RecordStamp {

        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "interaction_id")]
        public long InteractionId { get; set; }

        [JsonProperty(PropertyName = "drug_id")]
        public int PrescriptionId { get; set; }

        [JsonProperty(PropertyName = "drug")]
        public string Prescription { get; set; }

        [JsonProperty(PropertyName = "drug_name")]
        public string PrescriptionName { get; set; }

        [JsonProperty(PropertyName = "package")]
        public string Package { get; set; }

        [JsonProperty(PropertyName = "package_name")]
        public string PackageName { get; set; }

        [JsonProperty(PropertyName = "dosage_id")]
        public string DosageId { get; set; }

        [JsonProperty(PropertyName = "amount_per_30_days")]
        public int AmountPerThirtyDays { get; set; }

        [JsonProperty(PropertyName = "dated")]
        public DateTime Dated { get; set; }

        [JsonProperty(PropertyName = "drug_info_as_xml")]
        public string PrescriptionInfoAsXml { get; set; }

        [JsonProperty(PropertyName = "ndc")]
        public string NationalDrugCode { get; set; }

        public bool IsDeleted { get; set; }

        public long? QrsPrescriptionId { get; set; }
    }
}
