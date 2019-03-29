using System.Threading.Tasks;
using JetBrains.Annotations;
using Lykke.Common.Chaos;
using Lykke.Cqrs;
using Lykke.Job.NeoClaimTransactionsExecutor.Domain.Repositories;
using Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Commands;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Workflow.CommandHandlers
{
    public class ReleaseLockCommandHandler
    {
        private readonly IDistributedLocker _locker;
        private readonly IChaosKitty _chaosKitty;

        public ReleaseLockCommandHandler(IDistributedLocker locker, IChaosKitty chaosKitty)
        {
            _locker = locker;
            _chaosKitty = chaosKitty;
        }

        [UsedImplicitly]
        public async Task<CommandHandlingResult> Handle(ReleaseLockCommand command, IEventPublisher publisher)
        {
            await _locker.ReleaseLockAsync(command.TransactionId);

            _chaosKitty.Meow(command.TransactionId);

            return CommandHandlingResult.Ok();
        }
    }
}
