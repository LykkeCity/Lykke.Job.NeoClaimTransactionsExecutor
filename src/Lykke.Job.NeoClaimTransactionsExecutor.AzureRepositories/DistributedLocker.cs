using System;
using System.Threading.Tasks;
using AzureStorage;
using AzureStorage.Tables;
using Lykke.Common.Log;
using Lykke.Job.NeoClaimTransactionsExecutor.Domain.Repositories;
using Lykke.SettingsReader;

namespace Lykke.Job.NeoClaimTransactionsExecutor.AzureRepositories
{
    public class DistributedLocker:IDistributedLocker
    {
        private readonly INoSQLTableStorage<DistriibutedLockEntity> _storage;

        private DistributedLocker(INoSQLTableStorage<DistriibutedLockEntity> storage)
        {
            _storage = storage;
        }

        public static IDistributedLocker Create(IReloadingManager<string> connString, ILogFactory log)
        {
            return new DistributedLocker(AzureTableStorage<DistriibutedLockEntity>.Create(connString, "Locks", log));
        }

        public async Task<bool> TryLockAsync(Guid owner)
        {
            var partition = GeneratePartitionKey();
            var row = GenerateRowKey();

            return (await _storage.GetOrInsertAsync(partition, row,()=> new DistriibutedLockEntity
            {
                PartitionKey = partition,
                RowKey = row,
                Owner = owner
            })).Owner == owner;
        }

        public Task ReleaseLockAsync(Guid owner)
        {
            return _storage.DeleteIfExistAsync(GeneratePartitionKey(), 
                GenerateRowKey(), 
                (ent) => ent.Owner == owner);
        }

        private static string GeneratePartitionKey()
        {
            return "_";
        }

        public static string GenerateRowKey()
        {
            return "_";
        }
    }
}
