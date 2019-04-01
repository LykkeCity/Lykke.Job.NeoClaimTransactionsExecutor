using System;
using System.Collections.Generic;
using System.Text;
using MessagePack;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Events
{
    [MessagePackObject(keyAsPropertyName: true)]
    public class ClaimbaleGasNotAvailiableEvent
    {
        public Guid TransactionId { get; set; }
    }
}
