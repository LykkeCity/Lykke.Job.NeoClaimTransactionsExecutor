using System;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Commands
{
    public class WaitForTransactionEndingCommand
    {
        public Guid TransactionId { get; set; }
    }
}
