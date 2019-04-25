using System;
using Lykke.AzureStorage.Tables;

namespace Lykke.Job.NeoClaimTransactionsExecutor.AzureRepositories.DistributedLock
{
    internal class DistriibutedLockEntity:AzureTableEntity
    {
        public Guid Owner { get; set; }
    }
}
