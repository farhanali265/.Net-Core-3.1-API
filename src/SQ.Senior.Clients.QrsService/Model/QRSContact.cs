using Newtonsoft.Json;
namespace SQ.Senior.Clients.QrsService.Models {
    public class QRSContact : RecordStamp {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "interaction_id")]
        public long? InteractionId { get; set; }

        [JsonProperty(PropertyName = "contact_type_id")]
        public int ContactTypeId { get; set; }

        [JsonProperty(PropertyName = "address_line_1")]
        public string AddressLine1 { get; set; }

        [JsonProperty(PropertyName = "address_line_2")]
        public string AddressLine2 { get; set; }

        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }

        [JsonProperty(PropertyName = "county")]
        public string County { get; set; }

        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }

        [JsonProperty(PropertyName = "state_id")]
        public int? StateId { get; set; }

        [JsonProperty(PropertyName = "zip_code")]
        public string ZipCode { get; set; }

        [JsonProperty(PropertyName = "fips_code")]
        public string FipsCode { get; set; }

        [JsonProperty(PropertyName = "phone_number")]
        public string PhoneNumber { get; set; }

        [JsonProperty(PropertyName = "email_address")]
        public string EmailAddress { get; set; }

        public long QrsContactId { get; set; }
    }
}
