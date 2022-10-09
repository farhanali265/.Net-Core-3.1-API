using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using SQ.Extensions.Configuration.AwsSecretsManager;

namespace SQ.Senior.Quoting.External {
    public class Program {
        private const string SecretName = "SelectCare/Senior/QuotingServiceAPI";

        public static void Main(string[] args) {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) => {
                    config.AddAwsSecretsManager(SecretName);
                })
                .ConfigureWebHostDefaults(webBuilder => {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
