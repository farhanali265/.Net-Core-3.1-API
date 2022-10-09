using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SQ.Senior.Clients.QrsService;
using SQ.Senior.Quoting.External.Helpers;
using SQ.Senior.Quoting.External.Services;
using SQ.Senior.SpecialEnrollmentPeriods.DependencyInjection;
using SQ.Senior.Clients.FEMAService;

namespace SQ.Senior.Quoting.External {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddControllersWithViews();
            services.AddCustomDbContext(Configuration);
            services.AddInfrastructurServices();
            services.AddCustomConfiguration(Configuration);
            services.AddDRXApiClient(Configuration);
            services.AddNpiApiClient(Configuration);
            services.AddQrsApiClient(Configuration);
            services.AddHttpContext();
            services.AddDRXAuthentication(Configuration);
            services.AddSpecialEnrollmentPeriod(Configuration);
            services.AddMarxApiClient(Configuration);

            services.AddSwaggerDocument(config => {
                config.PostProcess = document => {
                    document.Info.Version = "v1";
                    document.Info.Title = "SQ Senior Quoting API";
                    document.Info.Description = "SQ Senior Quoting API";
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                // Register the Swagger generator and the Swagger UI middlewares
                app.UseOpenApi();
                app.UseSwaggerUi3();
            } else {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            QRSServiceConfig.Services = app.ApplicationServices;
            FEMAServiceConfig.Services = app.ApplicationServices;

            ContextAccessorHelper.Configure(app.ApplicationServices.GetRequiredService<IHttpContextAccessor>());
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
