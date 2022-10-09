using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SQ.DecisionTreeWorkflow.DatabaseAdapter;
using SQ.DecisionTreeWorkflow.DatabaseAdapter.Abstractions;
using SQ.DecisionTreeWorkflow.DatabaseAdapter.Repositories;
using SQ.DecisionTreeWorkflow.Services.Handlers;
using SQ.DecisionTreeWorkflow.Services.Mappers;
using SQ.Senior.Quoting.Internal.DatabaseAdapter;
using SQ.Senior.Quoting.Internal.DatabaseAdapter.Abstractions;
using SQ.Senior.Quoting.Internal.DatabaseAdapter.Repositories;
using SQ.Senior.SpecialEnrollmentPeriods.Handlers;

namespace SQ.Senior.SpecialEnrollmentPeriods.DependencyInjection {
    public static class SpecialEnrollmentPeriodMiddleware {
        public static IServiceCollection AddSpecialEnrollmentPeriod(this IServiceCollection services, IConfiguration configuration) {
            services.AddOptions();

            services.AddDbContextPool<IDecisionTreeWorkflowDbContext, DecisionTreeWorkflowDbContext>(options => {
                options.UseSqlServer(configuration.GetConnectionString(nameof(DecisionTreeWorkflowDbContext)));
                options.EnableSensitiveDataLogging();
                options.EnableDetailedErrors();
            });

            services.AddDbContextPool<ISQSeniorInternalQuotingDbContext, SQSeniorInternalQuotingDbContext>(options => {
                options.UseSqlServer(configuration.GetConnectionString(nameof(SQSeniorInternalQuotingDbContext)));
                options.EnableSensitiveDataLogging();
                options.EnableDetailedErrors();
            });

            services.AddScoped<IDecisionTreeWorkflowDbRepository, DecisionTreeWorkflowDbRepository>();
            services.AddScoped<ISeniorInternalQuotingDbRepository, SeniorInternalQuotingDbRepository>();
            services.AddScoped<IWorkflowMapper, WorkflowMapper>();
            services.AddScoped<IWorkflowHandler, WorkflowHandler>();
            services.AddScoped<IWizardHandler, WizardHandler>();
            services.AddScoped<IQuestionHandler, QuestionHandler>();

            return services;
        }
    }
}
