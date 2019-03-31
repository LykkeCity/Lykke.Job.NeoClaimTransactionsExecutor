using System;
using MessagePack;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Commands
{
    [MessagePackObject(keyAsPropertyName: true)]
    public class ClearTransactionCommand
    {
        public Guid TransactionId { get; set; }
    }
}
