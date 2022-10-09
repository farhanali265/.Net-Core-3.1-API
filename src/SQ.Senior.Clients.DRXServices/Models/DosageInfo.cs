using System.Collections.Generic;

namespace SQ.Senior.Clients.DrxServices.Models {

    public class DosageInfo {
        public string LabelName { get; set; }
        public int CommonDaysOfSupply { get; set; }
        public double CommonMetricQuantity { get; set; }
        public double CommonUserQuantity { get; set; }
        public bool IsCommonDosage { get; set; }
        public string ReferenceNDC { get; set; }
        public string DosageId { get; set; }
        public List<PrescriptionPackageInfo> Packages { get; set; }
        public string GenericDosageId { get; set; }
    }
}
