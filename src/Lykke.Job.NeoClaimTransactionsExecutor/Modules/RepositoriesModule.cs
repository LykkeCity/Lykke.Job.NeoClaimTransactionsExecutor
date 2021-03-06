﻿using Autofac;
using Lykke.Common.Log;
using Lykke.Job.NeoClaimTransactionsExecutor.AzureRepositories.DistributedLock;
using Lykke.Job.NeoClaimTransactionsExecutor.AzureRepositories.TransactionExecution;
using Lykke.Job.NeoClaimTransactionsExecutor.Domain.Domain;
using Lykke.Job.NeoClaimTransactionsExecutor.Domain.Repositories;
using Lykke.Job.NeoClaimTransactionsExecutor.Settings;
using Lykke.Job.NeoClaimTransactionsExecutor.Settings.JobSettings;
using Lykke.Job.NeoClaimTransactionsExecutor.Workflow;
using Lykke.SettingsReader;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Modules
{
    public class RepositoriesModule : Module
    {
        private readonly IReloadingManager<NeoClaimTransactionsExecutorJobSettings> _settingsManager;

        public RepositoriesModule(IReloadingManager<AppSettings> settingsManager)
        {
            _settingsManager = settingsManager.Nested(p=>p.NeoClaimTransactionsExecutorJob);
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => TransactionExecutionsRepository.Create(_settingsManager.Nested(x => x.Db.DataConnString), c.Resolve<ILogFactory>()))
                .As<ITransactionExecutionsRepository>()
                .SingleInstance();

            builder.Register(c => DistributedLocker.Create(_settingsManager.Nested(x => x.Db.DataConnString), c.Resolve<ILogFactory>()))
                .As<IDistributedLocker>()
                .SingleInstance();

            builder.Register(c => CommandHandlerEventRepository.Create(_settingsManager.Nested(x => x.Db.DataConnString),
                    c.Resolve<ILogFactory>(),
                    CommandHandlerEventConfigurer.ConfigureCapturedEvents()))
                .As<ICommandHandlerEventRepository>()
                .SingleInstance();
        }
    }
}
