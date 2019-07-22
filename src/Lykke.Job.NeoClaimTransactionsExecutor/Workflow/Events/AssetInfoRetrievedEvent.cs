using System;
using ProtoBuf;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Events
{
    [ProtoContract]
    public class AssetInfoRetrievedEvent
    {
        [ProtoMember(1)]
        public Guid TransactionId { get; set; }

        [ProtoMember(2)]
        public string NeoAssetId { get; set; }

        [ProtoMember(3)]
        public string GasAssetId { get; set; }

        [ProtoMember(4)]
        public string NeoBlockchainAssetId { get; set; }

        [ProtoMember(5)]
        public string NeoBlockchainType { get; set; }

        [ProtoMember(6)]
        public string GasBlockchainAssetId { get; set; }

        [ProtoMember(7)]
        public string GasBlockchainType { get; set; }
    }
}
