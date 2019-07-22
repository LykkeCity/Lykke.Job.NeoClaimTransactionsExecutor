using System;
using ProtoBuf;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Commands
{
    [ProtoContract]
    public class SignTransactionCommand
    {
        [ProtoMember(1)]
        public Guid TransactionId { get; set; }

        [ProtoMember(2)]
        public string Address { get; set; }

        [ProtoMember(3)]
        public string UnsignedTransactionContext { get; set; }

        [ProtoMember(4)]
        public string NeoBlockchainType { get; set; }
    }
}
