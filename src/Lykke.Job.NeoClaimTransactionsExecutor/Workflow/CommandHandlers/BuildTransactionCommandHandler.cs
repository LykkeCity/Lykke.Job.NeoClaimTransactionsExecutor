using System.Threading.Tasks;
using JetBrains.Annotations;
using Lykke.Common.Chaos;
using Lykke.Cqrs;
using Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Commands;
using Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Events;
using Lykke.Service.NeoApi.Client;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Workflow.CommandHandlers
{
    public class BuildTransactionCommandHandler
    {
        private readonly INeoClaimBuilderClient _claimBuilderClient;
        private readonly IChaosKitty _chaosKitty;

        public BuildTransactionCommandHandler(INeoClaimBuilderClient claimBuilderClient,
            IChaosKitty chaosKitty)
        {
            _claimBuilderClient = claimBuilderClient;
            _chaosKitty = chaosKitty;
        }

        [UsedImplicitly]
        public async Task<CommandHandlingResult> Handle(BuildTransactionCommand command,
            IEventPublisher publisher)
        {
            var res = await _claimBuilderClient.BuildClaimTransacionAsync(command.TransactionId, command.Address);
            
            _chaosKitty.Meow(command.TransactionId);

            publisher.PublishEvent(new TransactionBuiltEvent
            {
                TransactionId = command.TransactionId,
                AllGas = res.allGas,
                ClaimedGas = res.claimedGas,
                UnsignedTransactionContext = res.transactionContext
            });

            return CommandHandlingResult.Ok();
        }
    }
}
