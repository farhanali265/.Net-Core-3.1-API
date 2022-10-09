using System;
using System.Collections.Generic;

namespace SQ.Senior.Quoting.DatabaseAdapter.Models {

    public class Prescription {
        public Prescription() {
            this.PrescriptionDosages = new HashSet<PrescriptionDosage>();
        }

        public Guid ID { get; set; }
        public Guid? UserID { get; set; }
        public string DRXPrescriptionID { get; set; }
        public string PrescriptionName { get; set; }
        public string ChemicalName { get; set; }
        public int? PrescriptionTypeID { get; set; }
        public string PrescriptionType { get; set; }
        public string PrescriptionTypeNDA { get; set; }
        public DateTime? AddDate { get; set; }

        public virtual ICollection<PrescriptionDosage> PrescriptionDosages { get; set; }
    }
}
