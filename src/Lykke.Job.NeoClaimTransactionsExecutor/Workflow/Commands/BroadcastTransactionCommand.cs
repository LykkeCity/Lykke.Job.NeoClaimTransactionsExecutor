using System;
using MessagePack;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Commands
{
    [MessagePackObject(keyAsPropertyName: true)]
    public class BroadcastTransactionCommand
    {
        public Guid TransactionId { get; set; }

        public string SignedTransactionContext { get; set; }
    }
}
