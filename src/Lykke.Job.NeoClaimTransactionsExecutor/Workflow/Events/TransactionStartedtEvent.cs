using System;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Events
{
    public class TransactionStartedtEvent
    {
        public Guid TransactionId { get; set; }

        public string Address { get; set; }

        public string NeoAssetId { get; set; }

        public string GasAssetId { get; set; }
    }
}
