using System;
using MessagePack;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Commands
{
    [MessagePackObject(keyAsPropertyName: true)]
    public class BuildTransactionCommand
    {
        public Guid TransactionId { get; set; }

        public string Address { get; set; }
    }
}
