using System;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Commands
{
    public class BroadcastTransactionCommand
    {
        public Guid OperationId { get; set; }

        public string SignedTransactionContext { get; set; }
    }
}
