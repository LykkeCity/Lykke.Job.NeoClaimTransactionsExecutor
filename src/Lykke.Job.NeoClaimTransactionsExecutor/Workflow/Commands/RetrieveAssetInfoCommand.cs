using System;
using MessagePack;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Commands
{
    [MessagePackObject(keyAsPropertyName: true)]
    public class RetrieveAssetInfoCommand
    {
        public Guid TransactionId { get; set; }

        public string NeoAssetId { get; set; }

        public string GasAssetId { get; set; }
    }
}
