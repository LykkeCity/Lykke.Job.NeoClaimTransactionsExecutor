using Autofac;
using Autofac.Extensions.DependencyInjection;
using Lykke.Job.NeoClaimTransactionsExecutor.Services;
using Lykke.Job.NeoClaimTransactionsExecutor.Settings.JobSettings;
using Lykke.Sdk;
using Lykke.Sdk.Health;
using Lykke.SettingsReader;
using Microsoft.Extensions.DependencyInjection;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Modules
{
    public class JobModule : Module
    {
        private readonly NeoClaimTransactionsExecutorJobSettings _settings;
        private readonly IReloadingManager<NeoClaimTransactionsExecutorJobSettings> _settingsManager;
        // NOTE: you can remove it if you don't need to use IServiceCollection extensions to register service specific dependencies
        private readonly IServiceCollection _services;

        public JobModule(NeoClaimTransactionsExecutorJobSettings settings, IReloadingManager<NeoClaimTransactionsExecutorJobSettings> settingsManager)
        {
            _settings = settings;
            _settingsManager = settingsManager;

            _services = new ServiceCollection();
        }

        protected override void Load(ContainerBuilder builder)
        {
            // NOTE: Do not register entire settings in container, pass necessary settings to services which requires them
            // ex:
            // builder.RegisterType<QuotesPublisher>()
            //  .As<IQuotesPublisher>()
            //  .WithParameter(TypedParameter.From(_settings.Rabbit.ConnectionString))

            builder.RegisterType<HealthService>()
                .As<IHealthService>()
                .SingleInstance();

            builder.RegisterType<StartupManager>()
                .As<IStartupManager>()
                .SingleInstance();

            builder.RegisterType<ShutdownManager>()
                .As<IShutdownManager>()
                .AutoActivate()
                .SingleInstance();

            // TODO: Add your dependencies here

            builder.Populate(_services);
        }
    }
}
