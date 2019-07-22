using System;
using ProtoBuf;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Commands
{
    [ProtoContract]
    public class ClearTransactionCommand
    {
        [ProtoMember(1)]
        public Guid TransactionId { get; set; }
    }
}
