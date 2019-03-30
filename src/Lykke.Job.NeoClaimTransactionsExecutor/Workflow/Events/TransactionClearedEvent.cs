using System;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Events
{
    public class TransactionClearedEvent
    {
        public Guid TransactionId { get; set; }
    }
}
