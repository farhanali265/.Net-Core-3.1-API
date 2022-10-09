using SQ.Senior.Clients.QrsService;
using SQ.Senior.Clients.QrsService.Models;
using System;

namespace SQ.Senior.Quoting.External.Helpers {
    public class BrandHelper {
        public static BrandType GetBrandType() {

            var medicareHelplineHostName = QRSServiceConfig.OtherSettings.MedicareHelplineHostName;
            var hostName = ContextAccessorHelper.HttpContext.Request.Path;
            var isMedicareHelplineHost = Convert.ToString(hostName).Contains(medicareHelplineHostName);
            return isMedicareHelplineHost ? BrandType.MedicareHelpline : BrandType.SelectQuote;
        }
        public static bool IsMedicareHelpline() {
            var brandType = GetBrandType();
            return brandType == BrandType.MedicareHelpline;
        }
    }
}
