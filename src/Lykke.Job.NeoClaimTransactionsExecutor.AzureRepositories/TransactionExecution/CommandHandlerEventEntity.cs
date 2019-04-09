using System;
using Lykke.AzureStorage.Tables;

namespace Lykke.Job.NeoClaimTransactionsExecutor.AzureRepositories.TransactionExecution
{
    internal class CommandHandlerEventEntity: AzureTableEntity
    {
        public Guid AggregateId { get; set; }

        public Guid CorrelationId { get; set; }
        public string CommandHandlerId { get; set; }
        
        public string EventTypeKey { get; set; }

        public static string GeneratePartitionKey(Guid aggregateId)
        {
            return aggregateId.ToString();
        }

        public static string GenerateRowKey(string commandHandlerId)
        {
            return commandHandlerId;
        }
    }
}
