using System;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Commands
{
    public class StartTransactionCommand
    {
        public Guid TransactionId { get; set; }

        public string AssetId { get; set; }

        public string Address { get; set; }
    }
}
