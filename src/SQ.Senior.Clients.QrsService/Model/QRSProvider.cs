using Newtonsoft.Json;

namespace SQ.Senior.Clients.QrsService.Models {
    public class QRSProvider : RecordStamp {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "interaction_id")]
        public long? InteractionId { get; set; }

        [JsonProperty(PropertyName = "provider_type")]
        public string ProviderType { get; set; }

        [JsonProperty(PropertyName = "npi_number")]
        public long NpiNumber { get; set; }

        [JsonProperty(PropertyName = "specialty")]
        public string Speciality { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "address_line_1")]
        public string AddressLine1 { get; set; }

        [JsonProperty(PropertyName = "address_line_2")]
        public string AddressLine2 { get; set; }

        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }

        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }

        [JsonProperty(PropertyName = "phone")]
        public string Phone { get; set; }

        [JsonProperty(PropertyName = "is_active")]
        public bool IsActive { get; set; }
        public long? QrsProviderId { get; set; }
    }
}
