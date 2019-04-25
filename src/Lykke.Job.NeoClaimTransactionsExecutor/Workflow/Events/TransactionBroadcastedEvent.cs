using System;
using MessagePack;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Events
{
    [MessagePackObject(keyAsPropertyName: true)]
    public class TransactionBroadcastedEvent
    {
        public Guid TransactionId { get; set; }
    }
}
