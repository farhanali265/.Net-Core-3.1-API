namespace SQ.Senior.Clients.DrxServices.Configurations {

    public class DrxConfiguration {
        public string BaseUri { get; set; }
        public string TokenEndpoint { get; set; }
        public string PartnerId { get; set; }
        public string BrokerId { get; set; }
        public string AffiliateId { get; set; }
        public string ChannelId { get; set; }
        public string AgencyId { get; set; }
        public string AgencyName { get; set; }
        public int Timeout { get; set; }
        public string LinkAuthentication { get; set; }
        public string LinkSessionCreate { get; set; }
        public string LinkPrescriptionsSearch { get; set; }
        public string LinkPrescriptionsAutocomplete { get; set; }
        public string LinkPrescriptionsInfo { get; set; }
        public string LinkPlanSearch { get; set; }
        public string LinkPlanDetailSearch { get; set; }
        public string LinkPlanEnrollment { get; set; }
        public string LinkPharmacySearch { get; set; }
        public string LinkPlanPharmacySearch { get; set; }
        public string LinkRetailandMailOrderPharmacy { get; set; }
        public string LinkDefaultRetailPharmacy { get; set; }
        public string LinkAddRemovePharmacySession { get; set; }
        public string LinkFormularyTierPharmacy { get; set; }
        public string LinkGetQuestions { get; set; }
        public string LinkSubmitAnswers { get; set; }
        public string LinkGetPlans { get; set; }
        public string APIKey { get; set; }
        public string APIKeyAARP { get; set; }
        public string APIKeyMHL { get; set; }
        public string ShowErrorPopup { get; set; }
    }
}
