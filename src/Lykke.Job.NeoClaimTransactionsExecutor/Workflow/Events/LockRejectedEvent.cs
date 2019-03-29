using System;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Events
{
    public class LockRejectedEvent
    {
        public Guid TransactionId { get; set; }
    }
}
