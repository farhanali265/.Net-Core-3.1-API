using SQ.Senior.Clients.DrxServices.Models;
using System.IO;
using System.Xml.Serialization;

namespace SQ.Senior.Quoting.External.Services.Helpers {
    public static class PlanHelpers {
        public static PrescriptionInfo GetPrescriptionInfo(string prescriptionInfoAsXml) {

            if (!string.IsNullOrWhiteSpace(prescriptionInfoAsXml)) {
                using (StringReader responseAsString = new StringReader(prescriptionInfoAsXml)) {
                    XmlSerializer serializer =
                        new XmlSerializer(typeof(PrescriptionInfo));
                    PrescriptionInfo prescriptionInfo = (PrescriptionInfo)serializer.Deserialize(responseAsString);
                    return prescriptionInfo;
                }
            }
            return null;
        }
    }
}
