using System.Threading.Tasks;
using Common.Log;
using JetBrains.Annotations;
using Lykke.Common.Chaos;
using Lykke.Common.Log;
using Lykke.Cqrs;
using Lykke.Job.NeoClaimTransactionsExecutor.Domain.Domain;
using Lykke.Job.NeoClaimTransactionsExecutor.Domain.Repositories;
using Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Commands;
using Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Events;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Workflow.CommandHandlers
{
    public class AcquireLockCommandHandler
    {
        private readonly IDistributedLocker _locker;
        private readonly IChaosKitty _chaosKitty;
        private readonly ITransactionExecutionsRepository _repository;
        private readonly ILog _log;

        public AcquireLockCommandHandler(IDistributedLocker locker,
            IChaosKitty chaosKitty,
            ITransactionExecutionsRepository repository, 
            ILogFactory logFactory)
        {
            _locker = locker;
            _chaosKitty = chaosKitty;
            _repository = repository;
            _log = logFactory.CreateLog(this);
        }

        [UsedImplicitly]
        public async Task<CommandHandlingResult> Handle(AcquireLockCommand command, IEventPublisher publisher)
        {
            var transactionExecution = await _repository.GetAsync(command.TransactionId);

            if (transactionExecution.LockAcquired)
            {
                _log.Info("Address lock command has been skipped, since lock already was performed earlier", command);

                return CommandHandlingResult.Ok();
            }

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
