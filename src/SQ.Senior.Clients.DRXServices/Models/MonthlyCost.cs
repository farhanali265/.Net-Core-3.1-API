using System.Collections.Generic;

namespace SQ.Senior.Clients.DrxServices.Models {

    public class MonthlyCost {
        public int MonthId { get; set; }
        public List<MonthlyPrescriptionCostDetail> CostDetail { get; set; }
        public string CostPhases { get; set; }
        public decimal TotalMonthlyCost { get; set; }
    }
}
