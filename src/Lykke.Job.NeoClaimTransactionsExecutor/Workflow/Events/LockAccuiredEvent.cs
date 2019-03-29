using System;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Events
{
    public class LockAccuiredEvent
    {
        public Guid TransactionId { get; set; }
    }
}
