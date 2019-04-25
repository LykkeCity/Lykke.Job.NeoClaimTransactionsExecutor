using System;
using MessagePack;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Commands
{
    [MessagePackObject(keyAsPropertyName: true)]
    public class SignTransactionCommand
    {
        public Guid TransactionId { get; set; }

        public string Address { get; set; }

        public string UnsignedTransactionContext { get; set; }

        public string NeoBlockchainType { get; set; }
    }
}
