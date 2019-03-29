using System;
using System.Threading.Tasks;
using Common.Log;
using JetBrains.Annotations;
using Lykke.Common.Log;
using Lykke.Cqrs;
using Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Commands;
using Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Events;
using Lykke.Service.BlockchainApi.Client;
using Lykke.Service.BlockchainApi.Client.Models;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Workflow.CommandHandlers
{
    public class BroadcastTransactionCommandHandler
    {
        private readonly IBlockchainApiClient _client;
        private readonly ILog _log;

        public BroadcastTransactionCommandHandler(IBlockchainApiClient client,
            ILogFactory logFactory)
        {
            _client = client;
            _log = logFactory.CreateLog(this);
        }

        [UsedImplicitly]
        public async Task<CommandHandlingResult> Handle(BroadcastTransactionCommand command, IEventPublisher publisher)
        {
            var broadcastingResult = await _client.BroadcastTransactionAsync(command.TransactionId, command.SignedTransactionContext);

            switch (broadcastingResult)
            {
                case TransactionBroadcastingResult.Success:

                    publisher.PublishEvent(new TransactionBroadcastedEvent
                    {
                        TransactionId = command.TransactionId
                    });

                    return CommandHandlingResult.Ok();

                case TransactionBroadcastingResult.AlreadyBroadcasted:

                    _log.Info("API said that the transaction already was broadcasted", command);

                    publisher.PublishEvent(new TransactionBroadcastedEvent
                    {
                        TransactionId = command.TransactionId
                    });

                    return CommandHandlingResult.Ok();
                    
                default:
                    throw new ArgumentOutOfRangeException
                    (
                        nameof(broadcastingResult),
                        $"Transaction broadcastring result [{broadcastingResult}] is not supported."
                    );
            }
        }
    }
}
