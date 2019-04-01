using Autofac;
using Hangfire;
using Hangfire.Mongo;
using Lykke.Common.Chaos;
using Lykke.Common.Log;
using Lykke.Job.NeoClaimTransactionsExecutor.Services;
using Lykke.Job.NeoClaimTransactionsExecutor.Settings.JobSettings;
using Lykke.Logs.Hangfire;
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
                .WithParameter(TypedParameter.From(new ClaimGasStarterSettings(neoAssetId: _settings.NeoAssetId,
                        gasAssetId: _settings.GasAssetId,
                        claimTriggerCronExpression: _settings.ClaimTriggerCronExpression, 
                        neoHotWalletAddress: _settings.NeoHotWalletAddress)))
                .SingleInstance();

            builder.RegisterType<ShutdownManager>()
                .As<IShutdownManager>()
                .AutoActivate()
                .SingleInstance();

            builder
                .RegisterBuildCallback(StartHangfireServer)
                .Register(ctx => new BackgroundJobServer())
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


        private void StartHangfireServer(IContainer container)
        {
            var logProvider = new LykkeLogProvider(container.Resolve<ILogFactory>());

            var migrationOptions = new MongoMigrationOptions
            {
                Strategy = MongoMigrationStrategy.Migrate,
                BackupStrategy = MongoBackupStrategy.Collections
            };
            var storageOptions = new MongoStorageOptions
            {
                MigrationOptions = migrationOptions
            };

            GlobalConfiguration.Configuration.UseMongoStorage(
                _settings.Db.MongoConnString,
                "NeoClaimTransactionsExecutor", storageOptions);
            GlobalConfiguration.Configuration.UseLogProvider(logProvider)
                .UseAutofacActivator(container);

            container.Resolve<BackgroundJobServer>();
        }
    }
}
