using Newtonsoft.Json;
namespace SQ.Senior.Clients.QrsService.Models {
    public class QRSPharmacy : RecordStamp {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "interaction_id")]
        public long InteractionId { get; set; }

        [JsonProperty(PropertyName = "pharmacy_drx_id")]
        public string PharmacyDrxId { get; set; }

        [JsonProperty(PropertyName = "pharmacy_name")]
        public string PharmacyName { get; set; }

        [JsonProperty(PropertyName = "address_line_1")]
        public string Address1 { get; set; }

        [JsonProperty(PropertyName = "address_line_2")]
        public string Address2 { get; set; }

        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }

        [JsonProperty(PropertyName = "county")]
        public string Country { get; set; }

        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }

        [JsonProperty(PropertyName = "state_id")]
        public int StateId { get; set; }

        [JsonProperty(PropertyName = "zip_code")]
        public string ZipCode { get; set; }

        [JsonProperty(PropertyName = "state_code")]
        public string StateCode { get; set; }

        [JsonProperty(PropertyName = "phone")]
        public string Phone { get; set; }

        [JsonProperty(PropertyName = "latitude")]
        public string Latitude { get; set; }

        [JsonProperty(PropertyName = "longitude")]
        public string Longitude { get; set; }

        [JsonProperty(PropertyName = "distance")]
        public string Distance { get; set; }

        [JsonProperty(PropertyName = "pharmacy_npi")]
        public string PharmacyNPI { get; set; }

        [JsonProperty(PropertyName = "has_24hr_service")]
        public bool? Has24hrService { get; set; }

        [JsonProperty(PropertyName = "has_compounding")]
        public bool? HasCompounding { get; set; }

        [JsonProperty(PropertyName = "has_delivery")]
        public bool? HasDelivery { get; set; }

        [JsonProperty(PropertyName = "has_driveup")]
        public bool? HasDriveup { get; set; }

        [JsonProperty(PropertyName = "has_durable_equipment")]
        public bool? HasDurableEquipment { get; set; }

        [JsonProperty(PropertyName = "has_e_script")]
        public bool? HasEPrescriptions { get; set; }

        [JsonProperty(PropertyName = "has_handicap_access")]
        public bool? HasHandicapAccess { get; set; }

        [JsonProperty(PropertyName = "is_home_infusion")]
        public bool? IsHomeInfusion { get; set; }

        [JsonProperty(PropertyName = "is_ltc")]
        public bool? IsLongTermCare { get; set; }

        //[JsonProperty(PropertyName = "pharmacy_npi")]
        //public bool isDeleted { get; set; }

        [JsonProperty(PropertyName = "json_response")]
        public string JsonResponse { get; set; }
        public bool IsDeleted { get; set; }
        public long? QrsPharmacyId { get; set; }
    }
}
