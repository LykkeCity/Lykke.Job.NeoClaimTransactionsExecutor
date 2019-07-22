using System;
using ProtoBuf;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Events
{
    [ProtoContract]
    public class TransactionBuiltEvent
    {
        [ProtoMember(1)]
        public Guid TransactionId { get; set; }

        [ProtoMember(2)]
        public string UnsignedTransactionContext { get; set; }

        [ProtoMember(3)]
        public decimal ClaimedGas { get; set; }

        [ProtoMember(4)]
        public decimal AllGas { get; set; }
    }
}
