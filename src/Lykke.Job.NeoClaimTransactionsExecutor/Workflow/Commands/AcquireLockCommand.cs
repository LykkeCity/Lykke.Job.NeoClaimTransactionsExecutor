using System;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Commands
{
    public class AcquireLockCommand
    {
        public Guid TransactionId { get; set; }
    }
}
