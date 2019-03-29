using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Lykke.Common.Chaos;
using Lykke.Cqrs;
using Lykke.Job.NeoClaimTransactionsExecutor.Contract;
using Lykke.Job.NeoClaimTransactionsExecutor.Domain.Domain;
using Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Commands;
using Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Events;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Sagas
{
    public class TransactionExecutionSaga
    {
        private static string Self => NeoClaimTransactionsExecutorBoundedContext.Name;

        private readonly IAggregateRepository<TransactionExecutionAggregate> _repository;
        private readonly IChaosKitty _chaosKitty;

        public TransactionExecutionSaga(IAggregateRepository<TransactionExecutionAggregate> repository,
            IChaosKitty chaosKitty)
        {
            _repository = repository;
            _chaosKitty = chaosKitty;
        }

        [UsedImplicitly]
        private async Task Handle(TransactionStartedtEvent evt, ICommandSender sender)
        {
            var aggregate = await _repository.GetOrAddAsync(evt.TransactionId,
                () => TransactionExecutionAggregate.StartNew(evt.TransactionId, evt.Address, evt.AssetId));

            _chaosKitty.Meow(aggregate.TransactionId);

            sender.SendCommand(new AcquireLockCommand
            {
                TransactionId = aggregate.TransactionId
            }, Self);
        }

        [UsedImplicitly]
        private async Task Handle(LockAcquiredEvent evt, ICommandSender sender)
        {
            var aggregate = await _repository.GetAsync(evt.TransactionId);

            aggregate.OnLockAcquired(DateTime.UtcNow);
            await _repository.SaveAsync(aggregate);

            _chaosKitty.Meow(aggregate.TransactionId);

            sender.SendCommand(new RetrieveAssetInfoCommand
            {
                AssetId = aggregate.AssetId,
                TransactionId = aggregate.TransactionId
            }, Self);
        }

        [UsedImplicitly]
        private async Task Handle(LockRejectedEvent evt, ICommandSender sender)
        {
            var aggregate = await _repository.GetAsync(evt.TransactionId);

            aggregate.OnLockRejected(DateTime.UtcNow);
            await _repository.SaveAsync(aggregate);

            _chaosKitty.Meow(aggregate.TransactionId);
        }

        [UsedImplicitly]
        private async Task Handle(AssetInfoRetrievedEvent evt, ICommandSender sender)
        {
            var aggregate = await _repository.GetAsync(evt.TransactionId);

            aggregate.OnAssetInfoRetrieved(DateTime.UtcNow, 
                blockchainIntegrationLayerAssetId: evt.BlockchainIntegrationLayerAssetId,
                blockchainIntegrationLayerId: evt.BlockchainIntegrationLayerId);
            await _repository.SaveAsync(aggregate);

            _chaosKitty.Meow(aggregate.TransactionId);

            sender.SendCommand(new BuildTransactionCommand
            {
                TransactionId = aggregate.TransactionId,
                Address = aggregate.Address
            }, Self);

        }

        [UsedImplicitly]
        private async Task Handle(TransactionBuiltEvent evt, ICommandSender sender)
        {
            var aggregate = await _repository.GetAsync(evt.TransactionId);

            aggregate.OnTransactionBuilt(DateTime.UtcNow, evt.UnsignedTransactionContext, 
                allGas: evt.AllGas, 
                claimedGas: evt.ClaimedGas);
            await _repository.SaveAsync(aggregate);

            _chaosKitty.Meow(aggregate.TransactionId);

            sender.SendCommand(new SignTransactionCommand
            {
                TransactionId = aggregate.TransactionId,
                BlockchainIntegrationLayerId = aggregate.BlockchainIntegrationLayerId,
                UnsignedTransactionContext = aggregate.UnsignedTransactionContext,
                Address = aggregate.Address
            }, Self);
        }

        [UsedImplicitly]
        private async Task Handle(TransactionSignedEvent evt, ICommandSender sender)
        {
            var aggregate = await _repository.GetAsync(evt.TransactionId);

            aggregate.OnTransactionSigned(DateTime.UtcNow, evt.SignedTransactionContext);
            await _repository.SaveAsync(aggregate);

            _chaosKitty.Meow(aggregate.TransactionId);

            sender.SendCommand(new BroadcastTransactionCommand
            {
                TransactionId = aggregate.TransactionId,
                SignedTransactionContext = aggregate.SignedTransactionContext
            }, Self);
        }

        [UsedImplicitly]
        private async Task Handle(TransactionBroadcastedEvent evt, ICommandSender sender)
        {
            var aggregate = await _repository.GetAsync(evt.TransactionId);

            aggregate.OnTransactionBroadcasted(DateTime.UtcNow);
            await _repository.SaveAsync(aggregate);

            _chaosKitty.Meow(aggregate.TransactionId);

            sender.SendCommand(new WaitForTransactionEndingCommand
            {
                TransactionId = aggregate.TransactionId,
                Amount = aggregate.ClaimedGas ?? throw new ArgumentNullException(nameof(aggregate.ClaimedGas)),
                BlockchainIntegrationLayerAssetId = aggregate.BlockchainIntegrationLayerAssetId
            }, Self);
        }

        [UsedImplicitly]
        private async Task Handle(GasClaimTransactionExecutedEvent evt, ICommandSender sender)
        {
            var aggregate = await _repository.GetAsync(evt.TransactionId);

            aggregate.OnTransactionExecuted(evt.BroadcastingMoment, evt.TransactionHash, evt.BroadcastingBlock);
            await _repository.SaveAsync(aggregate);

            _chaosKitty.Meow(aggregate.TransactionId);
        }
    }
}
