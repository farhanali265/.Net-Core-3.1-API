using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SQ.Senior.Clients.QrsService.Models {
    public class ProviderDetail {
        public string id { get; set; }
        [JsonProperty("npi")]
        public int NationalProviderIdentifier { get; set; }
        public int entity_type_code { get; set; }
        public object replacement_npi { get; set; }
        public object provider_organization_name { get; set; }
        [JsonProperty("provider_last_name")]
        public string LastName { get; set; }
        public string provider_credential_text { get; set; }
        [JsonProperty("provider_first_name")]
        public string FirstName { get; set; }
        public string provider_middle_name { get; set; }
        public object npi_deactivation_date { get; set; }
        public object npi_reactivation_date { get; set; }
        public string provider_enumeration_date { get; set; }
        public string provider_gender_code { get; set; }
        public object authorized_official_first_name { get; set; }
        public object authorized_official_last_name { get; set; }
        public DateTime date_created { get; set; }
        public object date_modified { get; set; }
        public object date_deleted { get; set; }
        [JsonProperty("taxonomy")]
        public List<Taxonomy> Taxonomy { get; set; }
        [JsonProperty("contacts")]
        public List<Contact> Contacts { get; set; }
    }
    public class QRSSearchProvider {
        [JsonProperty("page")]
        public int Page { get; set; }
        [JsonProperty("limit")]
        public int Limit { get; set; }
        [JsonProperty("total")]
        public int Total { get; set; }
        [JsonProperty("results")]
        public List<ProviderDetail> ProviderDetails { get; set; }
    }
    public class Taxonomy {
        public DateTime date_created { get; set; }
        public string date_modified { get; set; }
        public string date_deleted { get; set; }
        public string id { get; set; }
        public string provider_id { get; set; }
        public string taxonomy_code { get; set; }
        public int is_primary { get; set; }
        [JsonProperty("npi_taxonomy_code")]
        public NpiTaxonomyCode NationalProviderIdentifierTaxonomyCode { get; set; }
    }

    public class Contact {
        public DateTime date_created { get; set; }
        public string date_modified { get; set; }
        public string date_deleted { get; set; }
        public string id { get; set; }
        public string provider_id { get; set; }
        [JsonProperty("contact_type_id")]
        public int ContactTypeId { get; set; }
        [JsonProperty("address_line_1")]
        public string Address1 { get; set; }
        [JsonProperty("address_line_2")]
        public string Address2 { get; set; }
        [JsonProperty("city")]
        public string City { get; set; }
        public string county { get; set; }
        [JsonProperty("state")]
        public string State { get; set; }
        public string state_id { get; set; }
        [JsonProperty("zip_code")]
        public string ZipCode { get; set; }
        public string fips_code { get; set; }
        [JsonProperty("phone_number")]
        public string PhoneNumber { get; set; }
        public string email_address { get; set; }
        public NpiContactType npi_contact_type { get; set; }
    }
    public class NpiTaxonomyCode {
        public string date_created { get; set; }
        public string date_modified { get; set; }
        public string date_deleted { get; set; }
        public string code { get; set; }
        public string category { get; set; }
        [JsonProperty("classification")]
        public string Classification { get; set; }
        [JsonProperty("specialization")]
        public string Specialization { get; set; }
        public string description { get; set; }
    }
    public class NpiContactType {
        public string date_created { get; set; }
        public string date_modified { get; set; }
        public string date_deleted { get; set; }
        public int id { get; set; }
        public string contact_type { get; set; }
        public int order_by { get; set; }
        public bool is_active { get; set; }
        public bool is_deleted { get; set; }
    }

    public class QRSProviderFilterCriteria {
        [JsonProperty("entity_type_code")]
        public int EntityTypeCode { get; set; } = 1;
        [JsonProperty("npi")]
        public int? NationalProviderIdentifier { get; set; }
        [JsonProperty("provider_first_name")]
        public string ProviderFirstName { get; set; } = string.Empty;
        [JsonProperty("provider_last_name")]
        public string ProviderLastName { get; set; } = string.Empty;
        [JsonProperty("city")]
        public string City { get; set; } = string.Empty;
        [JsonProperty("zip_code")]
        public string ZipCode { get; set; } = string.Empty;
        [JsonProperty("state")]
        public string State { get; set; } = string.Empty;
    }
}
