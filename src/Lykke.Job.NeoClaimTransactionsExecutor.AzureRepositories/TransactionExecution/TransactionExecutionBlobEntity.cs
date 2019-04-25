using System;
using Lykke.Job.NeoClaimTransactionsExecutor.Domain.Domain;

namespace Lykke.Job.NeoClaimTransactionsExecutor.AzureRepositories.TransactionExecution
{
    internal class TransactionExecutionBlobEntity
    {
        public string UnsignedTransactionContext { get; set; }
        
        public string SignedTransactionContext { get; set; }

        public static string GetContainerName()
        {
            return "neo-transaction-executions";
        }

        public static string GetBlobName(Guid operationId)
        {
            return operationId.ToString();
        }

        public static TransactionExecutionBlobEntity FromDomain(TransactionExecutionAggregate aggregate)
        {
            return new TransactionExecutionBlobEntity
            {
                UnsignedTransactionContext = aggregate.UnsignedTransactionContext,
                SignedTransactionContext = aggregate.SignedTransactionContext
            };
        }
    }
}
