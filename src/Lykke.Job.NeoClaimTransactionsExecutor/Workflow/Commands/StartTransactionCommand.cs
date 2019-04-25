using System;
using MessagePack;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Commands
{
    [MessagePackObject(keyAsPropertyName: true)]
    public class StartTransactionCommand
    {
        public Guid TransactionId { get; set; }

        public string NeoAssetId { get; set; }

        public string GasAssetId { get; set; }

        public string Address { get; set; }
    }
}
