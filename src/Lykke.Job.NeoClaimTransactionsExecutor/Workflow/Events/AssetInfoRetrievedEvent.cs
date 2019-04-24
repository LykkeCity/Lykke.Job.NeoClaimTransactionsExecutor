using System;
using MessagePack;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Events
{
    [MessagePackObject(keyAsPropertyName: true)]
    public class AssetInfoRetrievedEvent
    {
        public Guid TransactionId { get; set; }

        public string NeoAssetId { get; set; }

        public string GasAssetId { get; set; }

        public string NeoBlockchainIntegrationLayerAssetId { get; set; }

        public string NeoBlockchainIntegrationLayerId { get; set; }

        public string GasBlockchainIntegrationLayerAssetId { get; set; }

        public string GasBlockchainIntegrationLayerId { get; set; }
    }
}
