using System;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Settings.JobSettings
{
    public class CqrsSettings
    {
        public string RabbitConnectionString { get; set; }

        public TimeSpan RetryDelay { get; set; }

        public TimeSpan WaitForTransactionRetryDelay { get; set; }
    }
}
