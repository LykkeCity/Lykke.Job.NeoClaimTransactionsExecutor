using System;
using MessagePack;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Events
{
    [MessagePackObject(keyAsPropertyName: true)]
    public class TransactionStartedtEvent
    {
        public Guid TransactionId { get; set; }

        public string Address { get; set; }

        public string NeoAssetId { get; set; }

        public string GasAssetId { get; set; }
    }
}
