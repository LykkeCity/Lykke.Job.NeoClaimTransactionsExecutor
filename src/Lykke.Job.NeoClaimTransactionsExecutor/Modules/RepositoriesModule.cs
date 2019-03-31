using Autofac;
using Lykke.Common.Log;
using Lykke.Job.NeoClaimTransactionsExecutor.AzureRepositories.DistributedLock;
using Lykke.Job.NeoClaimTransactionsExecutor.AzureRepositories.TransactionExecution;
using Lykke.Job.NeoClaimTransactionsExecutor.Domain.Domain;
using Lykke.Job.NeoClaimTransactionsExecutor.Domain.Repositories;
using Lykke.Job.NeoClaimTransactionsExecutor.Settings.JobSettings;
using Lykke.SettingsReader;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Modules
{
    public class RepositoriesModule : Module
    {
        private readonly NeoClaimTransactionsExecutorJobSettings _settings;
        private readonly IReloadingManager<NeoClaimTransactionsExecutorJobSettings> _settingsManager;

        public RepositoriesModule(NeoClaimTransactionsExecutorJobSettings settings, IReloadingManager<NeoClaimTransactionsExecutorJobSettings> settingsManager)
        {
            _settings = settings;
            _settingsManager = settingsManager;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => TransactionExecutionsRepository.Create(_settingsManager.Nested(x => x.Db.DataConnString), c.Resolve<ILogFactory>()))
                .As<ITransactionExecutionsRepository>()
                .SingleInstance();

            builder.Register(c => DistributedLocker.Create(_settingsManager.Nested(x => x.Db.DataConnString), c.Resolve<ILogFactory>()))
                .As<IDistributedLocker>()
                .SingleInstance();
        }
    }
}
