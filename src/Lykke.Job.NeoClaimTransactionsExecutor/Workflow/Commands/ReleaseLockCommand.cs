using System;
using ProtoBuf;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Commands
{
    [ProtoContract]
    public class ReleaseLockCommand
    {
        [ProtoMember(1)]
        public Guid TransactionId { get; set; }
    }
}
