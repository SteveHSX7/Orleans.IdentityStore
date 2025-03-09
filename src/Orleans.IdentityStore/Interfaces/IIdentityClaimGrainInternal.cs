using System;
using System.Threading.Tasks;

namespace Orleans.IdentityStore.Interfaces
{
    internal interface IIdentityClaimGrainInternal : IIdentityClaimGrain
    {
        Task AddUserId(Guid id);

        Task RemoveUserId(Guid id);
    }
}