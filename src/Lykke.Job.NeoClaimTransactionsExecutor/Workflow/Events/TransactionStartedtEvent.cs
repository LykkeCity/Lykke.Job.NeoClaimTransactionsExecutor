using System;
using ProtoBuf;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Events
{
    [ProtoContract]
    public class TransactionStartedtEvent
    {
        [ProtoMember(1)]
        public Guid TransactionId { get; set; }

        [ProtoMember(2)]
        public string Address { get; set; }

        [ProtoMember(3)]
        public string NeoAssetId { get; set; }

        [ProtoMember(4)]
        public string GasAssetId { get; set; }
    }
}
