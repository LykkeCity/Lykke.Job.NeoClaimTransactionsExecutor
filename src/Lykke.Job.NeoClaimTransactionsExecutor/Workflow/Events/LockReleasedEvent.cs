using System;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Events
{
    public class LockReleasedEvent
    {
        public Guid TransactionId { get; set; }
    }
}
