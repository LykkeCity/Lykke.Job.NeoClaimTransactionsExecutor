using System.Threading.Tasks;
using JetBrains.Annotations;
using Lykke.Common.Chaos;
using Lykke.Cqrs;
using Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Commands;
using Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Events;
using Lykke.Service.Assets.Client;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Workflow.CommandHandlers
{
    public class RetrieveAssetInfoCommandHandler
    {
        private readonly IAssetsService _assetsService;
        private readonly IChaosKitty _chaosKitty;

        public RetrieveAssetInfoCommandHandler(IAssetsService assetsService, IChaosKitty chaosKitty)
        {
            _assetsService = assetsService;
            _chaosKitty = chaosKitty;
        }

        [UsedImplicitly]
        public async Task<CommandHandlingResult> Handle(RetrieveAssetInfoCommand command, IEventPublisher publisher)
        {
            var asset = await _assetsService.AssetGetAsync(command.AssetId);

            publisher.PublishEvent(new AssetInfoRetrievedEvent
            {
                TransactionId = command.TransactionId,
                AssetId = command.AssetId,
                BlockchainIntegrationLayerId = asset.BlockchainIntegrationLayerId,
                BlockchainIntegrationLayerAssetId = asset.BlockchainIntegrationLayerAssetId
            });

            _chaosKitty.Meow(command.TransactionId);

            return CommandHandlingResult.Ok();
        }
    }
}
