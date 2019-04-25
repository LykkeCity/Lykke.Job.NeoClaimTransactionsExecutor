using System;
using MessagePack;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Events
{
    [MessagePackObject(keyAsPropertyName: true)]
    public class LockReleasedEvent
    {
        public Guid TransactionId { get; set; }
    }
}
