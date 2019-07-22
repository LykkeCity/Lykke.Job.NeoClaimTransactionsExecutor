using System;
using ProtoBuf;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Commands
{
    [ProtoContract]
    public class BuildTransactionCommand
    {
        [ProtoMember(1)]
        public Guid TransactionId { get; set; }

        [ProtoMember(2)]
        public string Address { get; set; }
    }
}
