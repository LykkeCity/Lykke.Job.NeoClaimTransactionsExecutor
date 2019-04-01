using System;
using System.Threading.Tasks;
using Common.Log;
using JetBrains.Annotations;
using Lykke.Common.Chaos;
using Lykke.Common.Log;
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
        private readonly ILog _log;

        public BuildTransactionCommandHandler(INeoClaimBuilderClient claimBuilderClient,
            IChaosKitty chaosKitty,
            ILogFactory logFactory)
        {
            _claimBuilderClient = claimBuilderClient;
            _chaosKitty = chaosKitty;

            _log = logFactory.CreateLog(this);
        }

        [UsedImplicitly]
        public async Task<CommandHandlingResult> Handle(BuildTransactionCommand command,
            IEventPublisher publisher)
        {
            try
            {
                var res = await _claimBuilderClient.BuildClaimTransacionAsync(command.TransactionId, command.Address);

                publisher.PublishEvent(new TransactionBuiltEvent
                {
                    TransactionId = command.TransactionId,
                    AllGas = res.allGas,
                    ClaimedGas = res.claimedGas,
                    UnsignedTransactionContext = res.transactionContext
                });

            }
            catch (NeoClaimTransactionException e)
            {
                switch (e.ErrorType)
                {
                    case NeoClaimTransactionException.ErrorCode.ClaimableGasNotAvailiable:
                        _log.Info($"Api said that claimable gas not available",
                            context: new { txId = command.TransactionId });

                        publisher.PublishEvent(new ClaimbaleGasNotAvailiableEvent
                        {
                            TransactionId = command.TransactionId
                        });
                        break;

                    case NeoClaimTransactionException.ErrorCode.TransactionAlreadyBroadcased:
                        _log.Info($"Api said that claim transaction is already broadcasted - do nothing", 
                            context: new {txId =  command.TransactionId});
                        break;
                    default:
                        throw new ArgumentException($"Unknown switch {e.ErrorType}", nameof(e.ErrorType), e);
                }
            }

            _chaosKitty.Meow(command.TransactionId);


            return CommandHandlingResult.Ok();
        }
    }
}
