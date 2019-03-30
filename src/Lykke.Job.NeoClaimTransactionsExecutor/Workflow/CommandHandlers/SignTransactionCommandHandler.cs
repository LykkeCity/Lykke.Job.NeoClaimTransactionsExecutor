using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Lykke.Common.Chaos;
using Lykke.Cqrs;
using Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Commands;
using Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Events;
using Lykke.Service.BlockchainSignFacade.Client;
using Lykke.Service.BlockchainSignFacade.Contract.Models;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Workflow.CommandHandlers
{
    public class SignTransactionCommandHandler
    {
        private readonly IBlockchainSignFacadeClient _client;
        private readonly IChaosKitty _chaosKitty;

        public SignTransactionCommandHandler(IBlockchainSignFacadeClient client, 
            IChaosKitty chaosKitty)
        {
            _client = client;
            _chaosKitty = chaosKitty;
        }

        [UsedImplicitly]
        public async Task<CommandHandlingResult> Handle(SignTransactionCommand command, IEventPublisher publisher)
        {
            var transactionSigningResult = await _client.SignTransactionAsync
            (
                blockchainType: command.NeoBlockchainIntegrationLayerId,
                request: new SignTransactionRequest
                {
                    PublicAddresses = new[] { command.Address },
                    TransactionContext = command.UnsignedTransactionContext
                }
            );

            _chaosKitty.Meow(command.TransactionId);

            if (string.IsNullOrWhiteSpace(transactionSigningResult?.SignedTransaction))
            {
                throw new InvalidOperationException("Sign service returned the empty transaction");
            }

            publisher.PublishEvent(new TransactionSignedEvent
            {
                TransactionId = command.TransactionId,
                SignedTransactionContext = transactionSigningResult.SignedTransaction
            });

            return CommandHandlingResult.Ok();
        }
    }
}
