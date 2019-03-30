using System;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Commands
{
    public class RetrieveAssetInfoCommand
    {
        public Guid TransactionId { get; set; }

        public string NeoAssetId { get; set; }

        public string GasAssetId { get; set; }
    }
}
