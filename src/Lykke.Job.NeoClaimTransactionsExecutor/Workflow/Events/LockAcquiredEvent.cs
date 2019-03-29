using System;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Events
{
    public class LockAcquiredEvent
    {
        public Guid TransactionId { get; set; }
    }
}
