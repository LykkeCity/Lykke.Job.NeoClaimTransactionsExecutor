using System;
using MessagePack;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Commands
{
    [MessagePackObject(keyAsPropertyName: true)]
    public class WaitForTransactionEndingCommand
    {
        public Guid TransactionId { get; set; }
        
        public decimal Amount { get; set; }

        public string GasBlockchainAssetId { get; set; }
    }
}
