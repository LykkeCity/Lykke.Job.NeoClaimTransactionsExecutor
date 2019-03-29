using System.Threading.Tasks;
using JetBrains.Annotations;
using Lykke.Common.Chaos;
using Lykke.Cqrs;
using Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Commands;
using Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Events;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Workflow.CommandHandlers
{
    public class StartTransactionCommandHandler
    {
        private readonly IChaosKitty _chaosKitty;

        public StartTransactionCommandHandler(IChaosKitty chaosKitty)
        {
            _chaosKitty = chaosKitty;
        }

        [UsedImplicitly]
        public Task<CommandHandlingResult> Handle(StartTransactionCommand command, IEventPublisher publisher)
        {
            publisher.PublishEvent(new TransactionStartedtEvent
            {
                TransactionId = command.TransactionId,
                Address = command.Address,
                AssetId = command.AssetId
            });

            _chaosKitty.Meow(command.TransactionId);

            return Task.FromResult(CommandHandlingResult.Ok());
        }
    }
}
