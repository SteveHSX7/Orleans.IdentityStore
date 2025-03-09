using Microsoft.AspNetCore.Identity;
using Orleans.Concurrency;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Orleans.IdentityStore.Interfaces
{
    /// <summary>
    /// Identity Role grain
    /// </summary>
    /// <typeparam name="TUser">The user type</typeparam>
    /// <typeparam name="TRole">The role type</typeparam>
    public interface IIdentityRoleGrain<TUser, TRole> : IGrainWithGuidKey
        where TUser : IdentityUser<Guid>
        where TRole : IdentityRole<Guid>
    {
        /// <summary>
        /// Adds a claim to the role
        /// </summary>
        /// <param name="claim">The claim to add</param>
        Task AddClaim(IdentityRoleClaim<Guid> claim);

        /// <summary>
        /// Adds a user to the role
        /// </summary>
        /// <param name="id">The user to add</param>
        Task AddUser(Guid id);

        /// <summary>
        /// Creates the role
        /// </summary>
        /// <param name="role">The role to create</param>
        /// <returns>Result of the operations</returns>
        Task<IdentityResult> Create(TRole role);

        /// <summary>
        /// Delete the role
        /// </summary>
        Task<IdentityResult> Delete();

        /// <summary>
        /// Gets the role
        /// </summary>
        /// <returns>the role</returns>
        [AlwaysInterleave]
        Task<TRole> Get();

        /// <summary>
        /// Gets all the claims
        /// </summary>
        /// <returns>The list of claims</returns>
        [AlwaysInterleave]
        Task<IList<IdentityRoleClaim<Guid>>> GetClaims();

        /// <summary>
        /// Gets the users associated with this role
        /// </summary>
        /// <returns>A list of user ids associated with this role</returns>
        [AlwaysInterleave]
        Task<IList<Guid>> GetUsers();

        /// <summary>
        /// Removes a claim from this role
        /// </summary>
        /// <param name="claim">The claim to remove</param>
        Task RemoveClaim(Claim claim);

        /// <summary>
        /// Removes a user from this role
        /// </summary>
        /// <param name="id">The user to remove</param>
        Task RemoveUser(Guid id);

        /// <summary>
        /// Updates the role
        /// </summary>
        /// <param name="role">The updated role</param>
        /// <returns>Result of the operations</returns>
        Task<IdentityResult> Update(TRole role);
    }
}