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

        public string NeoBlockchainAssetId { get; set; }

        public string NeoBlockchainType { get; set; }

        public string GasBlockchainAssetId { get; set; }

        public string GasBlockchainType { get; set; }
    }
}
