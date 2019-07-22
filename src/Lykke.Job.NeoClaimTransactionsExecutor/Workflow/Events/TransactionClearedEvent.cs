using System;
using ProtoBuf;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Events
{
    [ProtoContract]
    public class TransactionClearedEvent
    {
        [ProtoMember(1)]
        public Guid TransactionId { get; set; }
    }
}
