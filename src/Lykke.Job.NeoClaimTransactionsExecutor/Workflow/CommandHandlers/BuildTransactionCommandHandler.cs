using System.Threading.Tasks;
using JetBrains.Annotations;
using Lykke.Cqrs;
using Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Commands;
using Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Events;
using Lykke.Service.NeoApi.Client;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Workflow.CommandHandlers
{
    public class BuildTransactionCommandHandler
    {
        private readonly INeoClaimBuilderClient _claimBuilderClient;

        public BuildTransactionCommandHandler(INeoClaimBuilderClient claimBuilderClient)
        {
            _claimBuilderClient = claimBuilderClient;
        }

        [UsedImplicitly]
        public async Task<CommandHandlingResult> Handle(BuildTransactionCommand command,
            IEventPublisher publisher)
        {
            var res = await _claimBuilderClient.BuildClaimTransacionAsync(command.TransactionId, command.Address);

            publisher.PublishEvent(new TransactionBuiltEvent
            {
                TransactionId = command.TransactionId,
                AllGas = res.allGas,
                ClaimedGas = res.claimedGas,
                TransactionContext = res.transactionContext
            });

            return CommandHandlingResult.Ok();
        }
    }
}
