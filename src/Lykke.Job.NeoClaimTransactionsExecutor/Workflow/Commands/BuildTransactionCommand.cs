using System;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Commands
{
    public class BuildTransactionCommand
    {
        public Guid OperationId { get; set; }

        public string Address { get; set; }
    }
}
