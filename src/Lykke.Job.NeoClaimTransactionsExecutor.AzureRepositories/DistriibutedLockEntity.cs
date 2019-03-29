using System;
using Lykke.AzureStorage.Tables;

namespace Lykke.Job.NeoClaimTransactionsExecutor.AzureRepositories
{
    public class DistriibutedLockEntity:AzureTableEntity
    {
        public Guid Owner { get; set; }
    }
}
