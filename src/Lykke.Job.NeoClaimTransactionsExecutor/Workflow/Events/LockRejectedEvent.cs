using System;
using ProtoBuf;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Events
{
    [ProtoContract]
    public class LockRejectedEvent
    {
        [ProtoMember(1)]
        public Guid TransactionId { get; set; }
    }
}
