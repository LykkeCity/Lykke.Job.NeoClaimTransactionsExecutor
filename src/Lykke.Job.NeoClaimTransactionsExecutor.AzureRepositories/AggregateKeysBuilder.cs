using System;

namespace Lykke.Job.NeoClaimTransactionsExecutor.AzureRepositories
{
    internal static class AggregateKeysBuilder
    {
        public static string BuildPartitionKey(Guid aggregateId)
        {
            return aggregateId.ToString();
        }

        public static string BuildRowKey()
        {
            return string.Empty;
        }
    }
}
