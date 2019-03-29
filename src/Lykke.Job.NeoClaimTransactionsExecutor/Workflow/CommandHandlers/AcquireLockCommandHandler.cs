using System.Threading.Tasks;
using JetBrains.Annotations;
using Lykke.Common.Chaos;
using Lykke.Cqrs;
using Lykke.Job.NeoClaimTransactionsExecutor.Domain.Repositories;
using Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Commands;
using Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Events;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Workflow.CommandHandlers
{
    public class AcquireLockCommandHandler
    {
        private readonly IDistributedLocker _locker;
        private readonly IChaosKitty _chaosKitty;

        public AcquireLockCommandHandler(IDistributedLocker locker, IChaosKitty chaosKitty)
        {
            _locker = locker;
            _chaosKitty = chaosKitty;
        }

        [UsedImplicitly]
        public async Task<CommandHandlingResult> Handle(AcquireLockCommand command, IEventPublisher publisher)
        {
            if (await _locker.TryLockAsync(command.TransactionId))
            {
                publisher.PublishEvent(new LockAcquiredEvent
                {
                    TransactionId = command.TransactionId
                });
            }
            else
            {
                publisher.PublishEvent(new LockRejectedEvent
                {
                    TransactionId = command.TransactionId
                });
            }

            _chaosKitty.Meow(command.TransactionId);

            return CommandHandlingResult.Ok();
        }
    }
}
