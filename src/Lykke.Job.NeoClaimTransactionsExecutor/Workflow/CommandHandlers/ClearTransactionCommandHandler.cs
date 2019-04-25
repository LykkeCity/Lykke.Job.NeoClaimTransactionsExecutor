using System.Threading.Tasks;
using JetBrains.Annotations;
using Lykke.Common.Chaos;
using Lykke.Cqrs;
using Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Commands;
using Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Events;
using Lykke.Service.BlockchainApi.Client;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Workflow.CommandHandlers
{
    public class ClearTransactionCommandHandler
    {
        private readonly IChaosKitty _chaosKitty;
        private readonly IBlockchainApiClient _client;

        public ClearTransactionCommandHandler(IChaosKitty chaosKitty, IBlockchainApiClient client)
        {
            _chaosKitty = chaosKitty;
            _client = client;
        }

        [UsedImplicitly]
        public async Task<CommandHandlingResult> Handle(ClearTransactionCommand command,
            IEventPublisher publisher)
        {
            await _client.ForgetBroadcastedTransactionsAsync(command.TransactionId);

            _chaosKitty.Meow(command.TransactionId);

            publisher.PublishEvent(new TransactionClearedEvent
            {
                TransactionId = command.TransactionId
            });

            return CommandHandlingResult.Ok();
        }
    }
}
