using System;
using ProtoBuf;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Commands
{
    [ProtoContract]
    public class RetrieveAssetInfoCommand
    {
        [ProtoMember(1)]
        public Guid TransactionId { get; set; }

        [ProtoMember(2)]
        public string NeoAssetId { get; set; }

        [ProtoMember(3)]
        public string GasAssetId { get; set; }
    }
}
