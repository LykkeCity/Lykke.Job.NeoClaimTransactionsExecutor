using System;
using MessagePack;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Contract
{
    [MessagePackObject(keyAsPropertyName: true)]
    public class GasClaimTransactionExecutedEvent
    {
        public Guid TransactionId { get; set; }
        public string TransactionHash { get; set; }
        public DateTime BroadcastingMoment { get; set; }
        public long BroadcastingBlock { get; set; }
        public decimal Amount { get; set; }
    }
}
