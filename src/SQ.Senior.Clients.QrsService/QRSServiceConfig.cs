using Microsoft.Extensions.Options;
using SQ.Senior.Clients.QrsService.QRSSettingsModel;
using System;

namespace SQ.Senior.Clients.QrsService {
    public static class QRSServiceConfig {

        static IServiceProvider services = null;
        public static IServiceProvider Services {
            get { return services; }
            set {
                if (services == null)
                    services = value;
            }
        }

        public static QRSSettings GetQRSSettings {
            get {
                var configService = services.GetService(typeof(IOptionsMonitor<QRSSettings>)) as IOptionsMonitor<QRSSettings>;
                return configService.CurrentValue;
            }
        }

        public static ProviderSettings GetProviderSettings {
            get {
                var configService = services.GetService(typeof(IOptionsMonitor<ProviderSettings>)) as IOptionsMonitor<ProviderSettings>;
                return configService.CurrentValue;
            }
        }
        public static OtherSettings OtherSettings {
            get {
                var configService = services.GetService(typeof(IOptionsMonitor<OtherSettings>)) as IOptionsMonitor<OtherSettings>;
                return configService.CurrentValue;
            }
        }

    }
}
