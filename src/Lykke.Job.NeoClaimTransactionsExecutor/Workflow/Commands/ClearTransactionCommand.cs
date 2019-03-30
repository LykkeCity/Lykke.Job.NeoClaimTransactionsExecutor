using System;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Commands
{
    public class ClearTransactionCommand
    {
        public Guid TransactionId { get; set; }
    }
}
