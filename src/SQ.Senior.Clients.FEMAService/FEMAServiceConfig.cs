using System;
using Microsoft.Extensions.Options;
using SQ.Senior.Clients.FEMAService.Models;

namespace SQ.Senior.Clients.FEMAService
{
    public static class FEMAServiceConfig {
        static IServiceProvider services = null;
        public static IServiceProvider Services {
            get { return services; }
            set {
                if (services == null)
                    services = value;
            }
        }

        public static FEMASettings FEMASettings {
            get {
                var configService = services.GetService(typeof(IOptionsMonitor<FEMASettings>)) as IOptionsMonitor<FEMASettings>;
                return configService.CurrentValue;
            }
        }
    }
}
