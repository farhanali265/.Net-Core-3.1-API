using System;

namespace SQ.Senior.Quoting.DatabaseAdapter.Models {

    public class PrescriptionDosage {
        public Guid ID { get; set; }
        public Guid PrescriptionID { get; set; }
        public string DosageDRXID { get; set; }
        public int? CommonDaysOfSupply { get; set; }
        public int? CommonMetricQuantity { get; set; }
        public int? CommonUserQuantity { get; set; }
        public bool? IsCommonDosage { get; set; }
        public string LabelName { get; set; }
        public string ReferenceNDC { get; set; }

        public virtual Prescription Prescription { get; set; }
    }
}