using System;
using System.Collections.Generic;
using System.Text;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Events
{
    public class TransactionClearedEvent
    {
        public Guid TransactionId { get; set; }
    }
}
