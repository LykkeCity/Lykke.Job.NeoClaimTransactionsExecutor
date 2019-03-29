using System;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Events
{
    public class AssetInfoRetrievedEvent
    {
        public Guid TransactionId { get; set; }

        public string AssetId { get; set; }

        public string BlockchainIntegrationLayerAssetId { get; set; }

        public string BlockchainIntegrationLayerId { get; set; }
    }
}
