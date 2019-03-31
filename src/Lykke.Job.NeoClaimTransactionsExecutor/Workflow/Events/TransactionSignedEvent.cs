using System;
using MessagePack;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Events
{
    [MessagePackObject(keyAsPropertyName: true)]
    public class TransactionSignedEvent
    {
        public Guid TransactionId { get; set; }

        public string SignedTransactionContext { get; set; }
    }
}
