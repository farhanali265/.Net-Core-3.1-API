namespace SQ.Senior.Quoting.DatabaseAdapter.Models {
    public class ZipFipsMapping {
        public int ID { get; set; }
        public string CountyName { get; set; }
        public string ZipCode { get; set; }
        public string ZipCodeType { get; set; }
        public string CountyFipsCode { get; set; }
        public string CityName { get; set; }
        public string StateCode { get; set; }
        public int AddressRecordCount { get; set; }
        public string PrevalentCountyFlag { get; set; }
        public string MultipleCountyFlag { get; set; }

        public virtual State State { get; set; }
    }
}