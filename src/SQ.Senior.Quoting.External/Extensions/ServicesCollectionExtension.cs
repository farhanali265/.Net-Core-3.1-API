using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using SQ.Senior.Clients.DrxServices.Configurations;
using SQ.Senior.Clients.DrxServices.Infrastructure.Services;
using SQ.Senior.Clients.DrxServices.Models;
using SQ.Senior.Clients.QrsService.Infrastructure.IQRSServices;
using SQ.Senior.Clients.QrsService.Infrastructure.Services;
using SQ.Senior.Clients.QrsService.QRSSettingsModel;
using SQ.Senior.Quoting.DatabaseAdapter;
using SQ.Senior.Quoting.DatabaseAdapter.Abstractions;
using SQ.Senior.Quoting.External.Services.IServices;
using System;
using SQ.Senior.Clients.FEMAService.Models;
using SQ.Senior.SpecialEnrollmentPeriods.Marx.Services;
using SQ.Senior.Clients.QrsService;

namespace SQ.Senior.Quoting.External.Services {
    public static class ServicesCollectionExtension {
        public static IServiceCollection AddInfrastructurServices(this IServiceCollection services) {
            services.AddScoped<ISQSeniorExternalQuotingDbContext, SQSeniorExternalQuotingDbContext>();
            services.AddScoped<IQrsService, QrsService>();
            services.AddScoped<IApplicantService, ApplicantService>();
            services.AddScoped<IPrescriptionService, PrescriptionService>();
            services.AddScoped<IPharmacyService, PharmacyService>();
            services.AddScoped<IUserPlanService, UserPlanService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProviderService, ProviderService>();
            services.AddScoped<IQrsProviderService, QrsProviderService>();
            services.AddScoped<IAuthentication, Authentication>();
            services.AddScoped<IQrsPrescriptionService, QrsPrescriptionService>();
            services.AddScoped<IZipFipsMappingService, ZipFipsMappingService>();
            services.AddScoped<IMarxService, MarxService>();
            services.AddScoped<IMarxApiClient, MarxApiClient>();
            services.AddScoped<IAppLogService, AppLogService>();
            return services;
        }
        public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration) {
            services.AddDbContextPool<SQSeniorExternalQuotingDbContext>(options => {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b => {
                    b.MigrationsAssembly("SQ.Senior.Quoting.External");
                });
            });
            return services;
        }

        public static IServiceCollection AddCustomConfiguration(this IServiceCollection services, IConfiguration configuration) {
            services.Configure<EmailSettings>(configuration.GetSection("SelectQuoteSettings:EmailSettings"));
            services.Configure<SelectQuoteCompanySettings>(configuration.GetSection("SelectQuoteSettings:SelectQuoteCompanySettings"));
            services.Configure<ProviderSettings>(configuration.GetSection("SelectQuoteSettings:ProviderSettings"));
            services.Configure<QRSSettings>(configuration.GetSection("SelectQuoteSettings:QRSSettings"));
            services.Configure<NpiSettings>(configuration.GetSection("SelectQuoteSettings:NpiSettings"));
            services.Configure<ZelisSettings>(configuration.GetSection("SelectQuoteSettings:ZelisSettings"));
            services.Configure<OtherSettings>(configuration.GetSection("SelectQuoteSettings:OtherSettings"));
            services.Configure<DrxConfiguration>(configuration.GetSection("SelectQuoteSettings:DRXSettings"));
            services.Configure<PhoneNumbers>(configuration.GetSection("PhoneNumbers"));
            services.Configure<FEMASettings>(configuration.GetSection("SelectQuoteSettings:FEMASettings"));
            return services;
        }

        public static IServiceCollection AddDRXApiClient(this IServiceCollection services, IConfiguration configuration) {
            services.AddHttpClient<IDrxApiClient, DrxApiClient>(client => {
                client.BaseAddress = new Uri(configuration.GetValue<string>("SelectQuoteSettings:DRXSettings:BaseUri"));
            });
            return services;
        }

        public static IServiceCollection AddDRXAuthentication(this IServiceCollection services, IConfiguration configuration) {
            services.AddHttpClient<IAuthentication, Authentication>(client => {
                client.BaseAddress = new Uri(configuration.GetValue<string>("SelectQuoteSettings:DRXSettings:LinkAuthentication"));
            }).AddTransientHttpErrorPolicy(policy => policy.WaitAndRetryAsync(2, _ => TimeSpan.FromSeconds(2)));
            return services;
        }
        public static IServiceCollection AddHttpContext(this IServiceCollection services) {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            return services;
        }

        public static IServiceCollection AddQrsApiClient(this IServiceCollection services, IConfiguration configuration) {
            services.AddHttpClient<IQrsApiClient, QrsApiClient>(client => {
                client.BaseAddress = new Uri(configuration.GetValue<string>("SelectQuoteSettings:QRSSettings:QRSAPIURL"));
                client.DefaultRequestHeaders.Add("x-api-key", configuration.GetValue<string>("SelectQuoteSettings:QRSSettings:QRSAPIKey"));
            });
            return services;
        }

        public static IServiceCollection AddNpiApiClient(this IServiceCollection services, IConfiguration configuration) {
            services.AddHttpClient<INpiApiClient, NpiApiClient>(client => {
                client.BaseAddress = new Uri(configuration.GetValue<string>("SelectQuoteSettings:NpiSettings:NpiApiUrl"));
                client.DefaultRequestHeaders.Add("x-api-key", configuration.GetValue<string>("SelectQuoteSettings:NpiSettings:NpiApiKey"));
            });
            return services;
        }

        public static IServiceCollection AddMarxApiClient(this IServiceCollection services, IConfiguration configuration) {
                services.AddHttpClient<IMarxApiClient, MarxApiClient>(client => {
                    client.BaseAddress = new Uri(configuration.GetValue<string>("SelectQuoteSettings:MarxSettings:BaseUri"));
                    client.DefaultRequestHeaders.Add("x-api-key", configuration.GetValue<string>("SelectQuoteSettings:MarxSettings:ApiKey"));
                });
            
            return services;
        }
    }
}
