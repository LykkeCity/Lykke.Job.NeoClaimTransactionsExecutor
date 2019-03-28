using System;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Events
{
    public class TransactionBroadcastedEvent
    {
        public Guid OperationId { get; set; }
    }
}
