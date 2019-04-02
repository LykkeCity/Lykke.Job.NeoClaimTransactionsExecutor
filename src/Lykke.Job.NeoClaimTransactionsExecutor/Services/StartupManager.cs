using System;
using System.Threading.Tasks;
using Common.Log;
using Hangfire;
using Lykke.Common.Log;
using Lykke.Cqrs;
using Lykke.Job.NeoClaimTransactionsExecutor.Contract;
using Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Commands;
using Lykke.Sdk;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Services
{
    public class StartupManager : IStartupManager
    {
        private readonly ILog _log;
        private readonly ClaimGasStarterSettings _starterSettings;
        private readonly ICqrsEngine _cqrsEngine;

        public StartupManager(ILogFactory logFactory, 
            ClaimGasStarterSettings starterSettings,
            ICqrsEngine cqrsEngine)
        {
            _starterSettings = starterSettings;
            _cqrsEngine = cqrsEngine;
            _log = logFactory.CreateLog(this);
        }

        public async Task StartAsync()
        {
            _cqrsEngine.StartAll();

            RecurringJob.AddOrUpdate("neo-claim-gas",
                () => StartClaimTransaction(),
                () => _starterSettings.ClaimTriggerCronExpression,
                TimeZoneInfo.Utc);

            await Task.CompletedTask;
        }

        public void StartClaimTransaction()
        {
            var transactionId = Guid.NewGuid();
            _log.Info($"Starting claim transaction {transactionId}", context: _starterSettings);

            _cqrsEngine.SendCommand(new StartTransactionCommand
            {
                TransactionId = transactionId,
                Address = _starterSettings.NeoHotWalletAddress,
                GasAssetId = _starterSettings.GasAssetId,
                NeoAssetId = _starterSettings.NeoAssetId
            }, NeoClaimTransactionsExecutorBoundedContext.Name, NeoClaimTransactionsExecutorBoundedContext.Name);
        }
    }
}
