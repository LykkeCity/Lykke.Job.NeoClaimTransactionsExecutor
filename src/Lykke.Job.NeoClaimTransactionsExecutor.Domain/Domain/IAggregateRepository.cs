﻿using System;
using System.Threading.Tasks;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Domain.Domain
{
    public interface IAggregateRepository<TAggregate>
    {
        Task<TAggregate> GetOrAddAsync(Guid aggregateId, Func<TAggregate> newAggregateFactory);
        Task<TAggregate> GetAsync(Guid operationId);
        Task SaveAsync(TAggregate aggregate);
        Task<TAggregate> TryGetAsync(Guid aggregateId);
    }
}
