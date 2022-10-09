namespace SQ.Senior.Quoting.External.Services.ViewModels.Plan {
    public class ProviderResponse {
        public int Id { get; set; }
        public string ProviderType { get; set; }
        public long NpiNumber { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Phone { get; set; }
        public bool Active { get; set; }
        public string Specialty { get; set; }
    }
}
