using System;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Commands
{
    public class WaitForTransactionEndingCommand
    {
        public Guid TransactionId { get; set; }
        
        public decimal Amount { get; set; }

        public string BlockchainIntegrationLayerAssetId { get; set; }
    }
}
