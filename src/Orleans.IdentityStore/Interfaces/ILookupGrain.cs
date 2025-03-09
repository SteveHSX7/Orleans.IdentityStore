using Orleans.Concurrency;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Orleans.IdentityStore.Interfaces
{
    internal interface ILookupGrain : IGrainWithStringKey
    {
        Task<bool> AddOrUpdate(string value, Guid grainKey);

        Task Delete(string value);

        Task DeleteIfMatch(string value, Guid grainKey);

        [AlwaysInterleave]
        Task<Guid?> Find(string value);

        [AlwaysInterleave]
        Task<IReadOnlyDictionary<string, Guid>> GetAll();
    }
}