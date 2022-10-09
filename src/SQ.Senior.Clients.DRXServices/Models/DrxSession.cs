using System;
using System.Xml.Serialization;
namespace SQ.Senior.Clients.DrxServices.Models {

    [XmlRoot(ElementName = "ArrayOfSession")]
    public class DrxSession {
        public DateTime Expires { get; set; }
        public string SessionId { get; set; }
        public string Status { get; set; }
        public int Reqid { get; set; }
    }
}
