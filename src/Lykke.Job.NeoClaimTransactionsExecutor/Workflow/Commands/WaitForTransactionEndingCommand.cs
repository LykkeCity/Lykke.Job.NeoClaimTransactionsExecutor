using System;
using ProtoBuf;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Commands
{
    [ProtoContract]
    public class WaitForTransactionEndingCommand
    {
        [ProtoMember(1)]
        public Guid TransactionId { get; set; }

        [ProtoMember(2)]
        public decimal Amount { get; set; }

        [ProtoMember(3)]
        public string GasBlockchainAssetId { get; set; }
    }
}
