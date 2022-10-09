namespace SQ.Senior.Quoting.External.Services.ViewModels.Plan {
    public class GetPlansRequest {
        public string UserId { get; set; }
        public string PurlToken { get; set; }
        public string ZipCode { get; set; }
        public string FipsCode { get; set; }
        public string PlanYear { get; set; }
        public int? ExtraHelpLevel { get; set; }
        public string PharmacyType { get; set; }
    }
}
