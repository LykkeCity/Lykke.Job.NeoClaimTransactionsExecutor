using System;
using MessagePack;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Events
{
    [MessagePackObject(keyAsPropertyName: true)]
    public class LockAcquiredEvent
    {
        public Guid TransactionId { get; set; }
    }
}
