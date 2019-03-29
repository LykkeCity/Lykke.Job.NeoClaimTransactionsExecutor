using System;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Events
{
    public class TransactionSignedEvent
    {
        public Guid TransactionId { get; set; }

        public string SignedTransactionContext { get; set; }
    }
}
