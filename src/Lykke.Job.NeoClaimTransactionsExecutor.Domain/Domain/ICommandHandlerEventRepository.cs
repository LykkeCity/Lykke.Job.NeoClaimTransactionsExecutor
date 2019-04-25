using System;
using System.Threading.Tasks;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Domain.Domain
{
    public interface ICommandHandlerEventRepository
    {
        Task<object> TryGetEventAsync(Guid aggregateId, string commandHandlerId);
        Task<T> InsertEventAsync<T>(Guid aggregateId, string commandHandlerId, T eventData);
    }
}
