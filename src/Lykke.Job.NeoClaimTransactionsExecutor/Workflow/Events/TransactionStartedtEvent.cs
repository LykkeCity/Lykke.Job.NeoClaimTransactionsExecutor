using System;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Events
{
    public class TransactionStartedtEvent
    {
        public Guid TransactionId { get; set; }

        public string Address { get; set; }

        public string AssetId { get; set; }
    }
}
