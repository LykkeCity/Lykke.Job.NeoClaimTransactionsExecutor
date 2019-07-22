using System;
using ProtoBuf;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Events
{
    [ProtoContract]
    public class ClaimbaleGasNotAvailiableEvent
    {
        [ProtoMember(1)]
        public Guid TransactionId { get; set; }
    }
}
