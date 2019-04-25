using System;
using MessagePack;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Events
{
    [MessagePackObject(keyAsPropertyName: true)]
    public class TransactionBuiltEvent
    {
        public Guid TransactionId { get; set; }

        public string UnsignedTransactionContext { get; set; }

        public decimal ClaimedGas { get; set; }

        public decimal AllGas { get; set; }
    }
}
