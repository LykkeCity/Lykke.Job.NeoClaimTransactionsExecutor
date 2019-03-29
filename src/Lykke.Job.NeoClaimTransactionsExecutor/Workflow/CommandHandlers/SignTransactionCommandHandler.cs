using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Lykke.Cqrs;
using Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Commands;
using Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Events;
using Lykke.Service.BlockchainSignFacade.Client;
using Lykke.Service.BlockchainSignFacade.Contract.Models;
using Lykke.Service.Assets.Client;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Workflow.CommandHandlers
{
    public class SignTransactionCommandHandler
    {
        private readonly IBlockchainSignFacadeClient _client;
        private readonly IAssetsService _assetsService;

        public SignTransactionCommandHandler(IBlockchainSignFacadeClient client, IAssetsService assetsService)
        {
            _client = client;
            _assetsService = assetsService;
        }

        [UsedImplicitly]
        public async Task<CommandHandlingResult> Handle(SignTransactionCommand command, IEventPublisher publisher)
        {
            var asset = await _assetsService.AssetGetAsync(command.AssetId);

            var transactionSigningResult = await _client.SignTransactionAsync
            (
                blockchainType: asset.BlockchainIntegrationLayerId,
                request: new SignTransactionRequest
                {
                    PublicAddresses = new[] { command.Address },
                    TransactionContext = command.UnsignedTransactionContext
                }
            );

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
