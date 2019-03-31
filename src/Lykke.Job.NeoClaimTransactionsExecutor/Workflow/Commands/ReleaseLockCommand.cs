using System;
using MessagePack;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Commands
{
    [MessagePackObject(keyAsPropertyName: true)]
    public class ReleaseLockCommand
    {
        public Guid TransactionId { get; set; }
    }
}
