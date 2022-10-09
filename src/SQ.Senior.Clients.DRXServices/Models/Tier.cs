using System;

namespace SQ.Senior.Clients.DrxServices.Models {

    public class Tier {
        public string Amount { get; set; }
        public string Description { get; set; }
        public decimal AmountAsDecimal {
            get {
                try {
                    return Convert.ToDecimal(Amount.Replace('$', '0'));
                } catch {
                    return -1;
                }
            }
        }
    }
}
