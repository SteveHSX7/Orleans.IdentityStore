using Orleans.Concurrency;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Orleans.IdentityStore.Interfaces
{
    /// <summary>
    /// Identity claim grain
    /// </summary>
    public interface IIdentityClaimGrain : IGrainWithStringKey
    {
        /// <summary>
        /// Get the users who have this claim
        /// </summary>
        /// <returns>The users have have claim</returns>
        [AlwaysInterleave]
        Task<IList<Guid>> GetUserIds();
    }
}