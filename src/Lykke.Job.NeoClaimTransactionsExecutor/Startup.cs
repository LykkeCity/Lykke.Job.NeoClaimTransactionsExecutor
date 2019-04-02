using JetBrains.Annotations;
using Lykke.Job.NeoClaimTransactionsExecutor.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using Lykke.Logs.Loggers.LykkeSlack;
using Lykke.Sdk;
using Microsoft.Extensions.Logging;

namespace Lykke.Job.NeoClaimTransactionsExecutor
{
    [UsedImplicitly]
    public class Startup
    {
        private readonly LykkeSwaggerOptions _swaggerOptions = new LykkeSwaggerOptions
        {
            ApiTitle = "NeoClaimTransactionsExecutorJob API",
            ApiVersion = "v1"
        };

        [UsedImplicitly]
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            return services.BuildServiceProvider<AppSettings>(options =>
            {
                options.SwaggerOptions = _swaggerOptions;

                options.Logs = logs =>
                {
                    logs.AzureTableName = "NeoClaimTransactionsExecutorJobLog";
                    logs.AzureTableConnectionStringResolver = settings => settings.NeoClaimTransactionsExecutorJob.Db.LogsConnString;

                    // TODO: You could add extended logging configuration here:

                    logs.Extended = p =>
                    {
                        p.AddAdditionalSlackChannel("CommonBlockChainIntegration");
                        p.AddAdditionalSlackChannel("CommonBlockChainIntegrationImportantMessages", x =>
                        {
                            x.MinLogLevel = LogLevel.Warning;
                        });
                    };

                };
            });
        }

        [UsedImplicitly]
        public void Configure(IApplicationBuilder app)
        {
            app.UseLykkeConfiguration(options =>
            {
                options.SwaggerOptions = _swaggerOptions;

                // TODO: Configure additional middleware for eg authentication or maintenancemode checks
                /*
                options.WithMiddleware = x =>
                {
                    x.UseMaintenanceMode<AppSettings>(settings => new MaintenanceMode
                    {
                        Enabled = settings.MaintenanceMode?.Enabled ?? false,
                        Reason = settings.MaintenanceMode?.Reason
                    });
                    x.UseAuthentication();
                };
                */
            });
        }
    }
}
