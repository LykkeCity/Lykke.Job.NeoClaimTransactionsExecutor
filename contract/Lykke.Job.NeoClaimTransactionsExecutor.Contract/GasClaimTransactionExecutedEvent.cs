using System;
using ProtoBuf;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Contract
{
    [ProtoContract]
    public class GasClaimTransactionExecutedEvent
    {
        [ProtoMember(1)]
        public Guid TransactionId { get; set; }
        [ProtoMember(2)]
        public string TransactionHash { get; set; }
        [ProtoMember(3)]
        public DateTime BroadcastingMoment { get; set; }
        [ProtoMember(4)]
        public long BroadcastingBlock { get; set; }
        [ProtoMember(5)]
        public decimal Amount { get; set; }
    }
}
