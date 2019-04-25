using System;
using System.Threading.Tasks;
using Common.Log;
using JetBrains.Annotations;
using Lykke.Common.Chaos;
using Lykke.Common.Log;
using Lykke.Cqrs;
using Lykke.Job.NeoClaimTransactionsExecutor.Domain.Domain;
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
        private readonly ICommandHandlerEventRepository _commandHandlerEventRepository;
        private const string CommandHandlerId = "BuildTransactionCommandsHandler";

        public BuildTransactionCommandHandler(INeoClaimBuilderClient claimBuilderClient,
            IChaosKitty chaosKitty,
            ILogFactory logFactory, 
            ICommandHandlerEventRepository commandHandlerEventRepository)
        {
            _claimBuilderClient = claimBuilderClient;
            _chaosKitty = chaosKitty;
            _commandHandlerEventRepository = commandHandlerEventRepository;

            _log = logFactory.CreateLog(this);
        }

        [UsedImplicitly]
        public async Task<CommandHandlingResult> Handle(BuildTransactionCommand command,
            IEventPublisher publisher)
        {
            var alreadyPublishedEvt = await _commandHandlerEventRepository.TryGetEventAsync(command.TransactionId,
                CommandHandlerId);

            if (alreadyPublishedEvt != null)
            {
                publisher.PublishEvent(alreadyPublishedEvt);

                return CommandHandlingResult.Ok();
            }
            try
            {
                var res = await _claimBuilderClient.BuildClaimTransacionAsync(command.TransactionId, command.Address);

                publisher.PublishEvent(await _commandHandlerEventRepository.InsertEventAsync(command.TransactionId,
                    CommandHandlerId,
                    new TransactionBuiltEvent
                    {
                        TransactionId = command.TransactionId,
                        AllGas = res.allGas,
                        ClaimedGas = res.claimedGas,
                        UnsignedTransactionContext = res.transactionContext
                    }));
            }
            catch (NeoClaimTransactionException e)
            {
                switch (e.ErrorType)
                {
                    case NeoClaimTransactionException.ErrorCode.ClaimableGasNotAvailiable:
                        _log.Info($"Api said that claimable gas not available",
                            context: new { txId = command.TransactionId });

                        publisher.PublishEvent(await _commandHandlerEventRepository.InsertEventAsync(command.TransactionId,
                            CommandHandlerId,
                            new ClaimbaleGasNotAvailiableEvent
                            {
                                TransactionId = command.TransactionId
                            }));

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
