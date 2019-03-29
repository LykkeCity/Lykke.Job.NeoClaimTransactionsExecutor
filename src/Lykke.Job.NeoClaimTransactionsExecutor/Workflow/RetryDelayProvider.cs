using System;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Workflow
{
    public class RetryDelayProvider
    {
        public TimeSpan WaitForTransactionRetryDelay { get; }

        public RetryDelayProvider(TimeSpan waitForTransactionRetryDelay)
        {
            WaitForTransactionRetryDelay = waitForTransactionRetryDelay;
        }
    }
}
