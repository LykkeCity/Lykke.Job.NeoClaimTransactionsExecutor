using System;
using MessagePack;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Commands
{
    public class AcquireLockCommand
    {
        [MessagePackObject(keyAsPropertyName: true)]
        public Guid TransactionId { get; set; }
    }
}
