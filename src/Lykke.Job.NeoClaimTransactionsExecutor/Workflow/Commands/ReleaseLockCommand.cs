using System;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Commands
{
    public class ReleaseLockCommand
    {
        public Guid TransactionId { get; set; }
    }
}
