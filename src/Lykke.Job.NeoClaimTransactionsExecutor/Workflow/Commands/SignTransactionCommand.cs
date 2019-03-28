using System;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Commands
{
    public class SignTransactionCommand
    {
        public Guid OperationId { get; set; }

        public string Address { get; set; }

        public string UnsignedTransactionContext { get; set; }
    }
}
