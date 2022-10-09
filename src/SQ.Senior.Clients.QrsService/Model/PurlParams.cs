namespace SQ.Senior.Clients.QrsService.Models {
    [PurlPropertyValidation(ErrorMessage = "You must supply at least one value")]
    public class PurlParams {
        public string Token { get; set; }
        public string Keyword { get; set; }
        public string ZipCode { get; set; }
        public string FipsCode { get; set; }
        public string AccountId { get; set; }
        public string IndividualId { get; set; }
        public string ScreenSize { get; set; }
        public string DeviceType { get; set; }
    }
}
