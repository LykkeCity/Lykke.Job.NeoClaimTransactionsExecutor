using System;
using System.Threading.Tasks;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Domain.Repositories
{
    public interface IDistributedLocker
    {
        Task<bool> TryLockAsync(Guid owner);

        Task ReleaseLockAsync(Guid owner);
    }
}
