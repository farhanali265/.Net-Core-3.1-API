namespace SQ.Senior.Clients.DrxServices.Models {

    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.datacontract.org/2004/07/CoreServices.PlanInfo")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.datacontract.org/2004/07/CoreServices.PlanInfo", IsNullable = false)]
    public class PlanFormularyTier {
        public object[] CopayPrices { get; set; }
    }
}
