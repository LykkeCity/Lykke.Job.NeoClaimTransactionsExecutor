using System;
using ProtoBuf;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Events
{
    [ProtoContract]
    public class LockReleasedEvent
    {
        [ProtoMember(1)]
        public Guid TransactionId { get; set; }
    }
}
