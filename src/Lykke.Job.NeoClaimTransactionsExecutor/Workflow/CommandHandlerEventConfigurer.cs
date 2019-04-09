using System;
using System.Collections.Generic;
using System.Text;
using Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Events;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Workflow
{
    class CommandHandlerEventConfigurer
    {
        public static ICollection<(string eventKey, Type eventType)> ConfigureCapturedEvents()
        {
            return new[]
            {
                ("TransactionBuiltEvent", typeof(TransactionBuiltEvent)),
                ("ClaimbaleGasNotAvailiableEvent", typeof(ClaimbaleGasNotAvailiableEvent))
            };
        }
    }
}
