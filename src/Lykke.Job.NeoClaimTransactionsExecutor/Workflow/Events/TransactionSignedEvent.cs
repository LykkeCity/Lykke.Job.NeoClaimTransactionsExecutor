using System;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Events
{
    public class TransactionSignedEvent
    {
        public Guid OperationId { get; set; }

        public string SignedTransactionContext { get; set; }
    }
}
