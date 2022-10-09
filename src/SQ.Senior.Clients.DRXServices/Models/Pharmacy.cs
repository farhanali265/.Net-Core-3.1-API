namespace SQ.Senior.Clients.DrxServices.Models {

    public class Pharmacy {
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string PharmacyPhone { get; set; }
        public double Distance { get; set; }
        public string PharmacyId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Chain { get; set; }
        public string ChainName { get; set; }
        public string PharmacyNPI { get; set; }
        public PharmacyCoverage PharmacyCoverage { get; set; }
        public PharmacyServices PharmacyServices { get; set; }
    }
}
