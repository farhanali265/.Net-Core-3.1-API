using Newtonsoft.Json;
namespace SQ.Senior.Clients.QrsService.Models {
    public class QRSPlan : RecordStamp {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "interaction_id")]
        public long InteractionId { get; set; }

        [JsonProperty(PropertyName = "contract_id")]
        public string ContactId { get; set; }

        [JsonProperty(PropertyName = "plan_id")]
        public string PlanId { get; set; }

        [JsonProperty(PropertyName = "segment_id")]
        public string SegmentId { get; set; }

        [JsonProperty(PropertyName = "drx_session_id")]
        public string DrxSessionId { get; set; }

        [JsonProperty(PropertyName = "drx_plan_id")]
        public string DrxPlanId { get; set; }

        [JsonProperty(PropertyName = "plan_name")]
        public string PlanName { get; set; }

        [JsonProperty(PropertyName = "plan_amount")]
        public decimal? PlanAmount { get; set; }

        [JsonProperty(PropertyName = "effective_year")]
        public int EffectiveYear { get; set; }

        [JsonProperty(PropertyName = "is_selected")]
        public bool IsSelected { get; set; }

        [JsonProperty(PropertyName = "carrier_name")]
        public string CarrierName { get; set; }

        [JsonProperty(PropertyName = "drug_deductible")]
        public decimal? PrescriptionDeductible { get; set; }

        [JsonProperty(PropertyName = "drug_premium")]
        public decimal? PrescriptionPremium { get; set; }

        [JsonProperty(PropertyName = "est_annual_drug_cost")]
        public decimal? EstAnnualPrescriptionCost { get; set; }

        [JsonProperty(PropertyName = "est_annual_medical_cost")]
        public decimal? EstAnnualMedicalCost { get; set; }

        [JsonProperty(PropertyName = "max_out_of_pocket")]
        public decimal? MaxOutOfPocket { get; set; }

        [JsonProperty(PropertyName = "medical_deductible")]
        public decimal? MedicalDeductible { get; set; }

        [JsonProperty(PropertyName = "medical_premium")]
        public decimal? MedicalPremium { get; set; }

        [JsonProperty(PropertyName = "plan_rating")]
        public decimal? PlanRating { get; set; }

        [JsonProperty(PropertyName = "plan_sub_type")]
        public string PlanSubType { get; set; }

        [JsonProperty(PropertyName = "plan_type")]
        public string PlanType { get; set; }

        [JsonProperty(PropertyName = "logo_name")]
        public string LogoName { get; set; }

        public bool IsDelete { get; set; }

        public long? QrsPlanId { get; set; }
    }
}
