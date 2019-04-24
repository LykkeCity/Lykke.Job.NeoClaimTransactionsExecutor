using System;
using System.Threading.Tasks;
using Common.Log;
using JetBrains.Annotations;
using Lykke.Common.Chaos;
using Lykke.Common.Log;
using Lykke.Cqrs;
using Lykke.Job.NeoClaimTransactionsExecutor.Contract;
using Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Commands;
using Lykke.Service.BlockchainApi.Client;
using Lykke.Service.BlockchainApi.Contract.Transactions;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Workflow.CommandHandlers
{
    public class WaitForTransactionEndingCommandHandler
    {
        private readonly RetryDelayProvider _retryDelayProvider;
        private readonly IBlockchainApiClient _client;
        private readonly ILog _log;
        private readonly IChaosKitty _chaosKitty;

        public WaitForTransactionEndingCommandHandler(RetryDelayProvider retryDelayProvider, 
            IBlockchainApiClient client, 
            ILogFactory logFactory, 
            IChaosKitty chaosKitty)
        {
            _retryDelayProvider = retryDelayProvider;
            _client = client;
            _log = logFactory.CreateLog(this);
            _chaosKitty = chaosKitty;
        }

        [UsedImplicitly]
        public async Task<CommandHandlingResult> Handle(WaitForTransactionEndingCommand command,
            IEventPublisher publisher)
        {
            var blockchainAsset = await _client.GetAssetAsync(command.GasBlockchainAssetId);

            var tx = await _client.TryGetBroadcastedSingleTransactionAsync(command.TransactionId, blockchainAsset);

            _chaosKitty.Meow(command.TransactionId);

            if (tx == null)
            {
                _log.Info("Blockchain API returned no transaction. Assuming, that it's already was cleared", command);

                // Transaction already has been forgotten, this means, 
                // that process has been went further and no events should be generated here.

                return CommandHandlingResult.Ok();
            }

            switch (tx.State)
            {
                case BroadcastedTransactionState.InProgress:

                    return CommandHandlingResult.Fail(_retryDelayProvider.WaitForTransactionRetryDelay);

                case BroadcastedTransactionState.Completed:

                    publisher.PublishEvent(new GasClaimTransactionExecutedEvent
                    {
                        TransactionId = command.TransactionId,
                        Amount = command.Amount,
                        BroadcastingBlock = tx.Block,
                        BroadcastingMoment = tx.Timestamp,
                        TransactionHash = tx.Hash
                    });

                    return CommandHandlingResult.Ok();
                default:
                    throw new ArgumentOutOfRangeException
                    (
                        nameof(tx.State),
                        $"Transaction state [{tx.State}] is not supported."
                    );
            }
        }
    }
}
