namespace SQ.Senior.Quoting.External.Services.ViewModels.Providers {
    public class SearchProvidersRequest {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string State { get; set; }
        public string NationalProviderIdentifier { get; set; }
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 10;
    }
}
