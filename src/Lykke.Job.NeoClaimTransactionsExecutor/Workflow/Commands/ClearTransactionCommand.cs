using System;
using System.Collections.Generic;
using System.Text;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Commands
{
    public class ClearTransactionCommand
    {
        public Guid TransactionId { get; set; }
    }
}
