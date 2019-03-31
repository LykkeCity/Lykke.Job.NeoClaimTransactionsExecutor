using Autofac;
using Lykke.Common.Chaos;
using Lykke.Job.NeoClaimTransactionsExecutor.Services;
using Lykke.Job.NeoClaimTransactionsExecutor.Settings.JobSettings;
using Lykke.Sdk;
using Lykke.Sdk.Health;
using Lykke.Service.Assets.Client;
using Lykke.SettingsReader;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Modules
{
    public class JobModule : Module
    {
        private readonly NeoClaimTransactionsExecutorJobSettings _settings;
        private readonly IReloadingManager<NeoClaimTransactionsExecutorJobSettings> _settingsManager;

        public JobModule(NeoClaimTransactionsExecutorJobSettings settings, IReloadingManager<NeoClaimTransactionsExecutorJobSettings> settingsManager)
        {
            _settings = settings;
            _settingsManager = settingsManager;
        }

        protected override void Load(ContainerBuilder builder)
        {
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

            builder.RegisterAssetsClient
            (
                new AssetServiceSettings
                {
                    ServiceUrl = _settings.AssetsServiceUrl
                }
            );
            
            builder.RegisterChaosKitty(_settings.ChaosKitty);
        }
    }
}
