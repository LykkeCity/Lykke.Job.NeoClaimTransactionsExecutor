using System;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Commands
{
    public class BuildTransactionCommand
    {
        public Guid TransactionId { get; set; }

        public string Address { get; set; }
    }
}
