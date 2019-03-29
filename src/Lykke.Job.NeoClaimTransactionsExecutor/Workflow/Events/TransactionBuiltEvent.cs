using System;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Events
{
    public class TransactionBuiltEvent
    {
        public Guid TransactionId { get; set; }

        public string TransactionContext { get; set; }

        public decimal ClaimedGas { get; set; }

        public decimal AllGas { get; set; }
    }
}
