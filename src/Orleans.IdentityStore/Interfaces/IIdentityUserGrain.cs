using Microsoft.AspNetCore.Identity;
using Orleans.Concurrency;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Orleans.IdentityStore.Interfaces
{
    /// <summary>
    /// Identity user grain
    /// </summary>
    /// <typeparam name="TUser">The user type</typeparam>
    /// <typeparam name="TRole">The role type</typeparam>
    public interface IIdentityUserGrain<TUser, TRole> : IGrainWithGuidKey
        where TUser : IdentityUser<Guid>
        where TRole : IdentityRole<Guid>
    {
        /// <summary>
        /// Add claims to user
        /// </summary>
        /// <param name="claims">The list of claims to add</param>
        Task AddClaims(IList<IdentityUserClaim<Guid>> claims);

        /// <summary>
        /// Add a login to user
        /// </summary>
        /// <param name="login">The login to add</param>
        Task AddLogin(IdentityUserLogin<Guid> login);

        /// <summary>
        /// Add a token to user
        /// </summary>
        /// <param name="token">The token to add</param>
        Task AddToken(IdentityUserToken<Guid> token);

        /// <summary>
        /// Adds a role to a user
        /// </summary>
        /// <param name="roleId">The id of the role to add</param>
        Task AddToRole(Guid roleId);

        /// <summary>
        /// Check if user has role
        /// </summary>
        /// <param name="id">The role id</param>
        /// <returns>true if the user has role, false otherwise</returns>
        [AlwaysInterleave]
        Task<bool> ContainsRole(Guid id);

        /// <summary>
        /// Creates the user
        /// </summary>
        /// <param name="user">The user data</param>
        /// <returns>Result of the operations</returns>
        Task<IdentityResult> Create(TUser user);

        /// <summary>
        /// Deletes user
        /// </summary>
        Task<IdentityResult> Delete();

        /// <summary>
        /// Get the user
        /// </summary>
        /// <returns>The user</returns>
        [AlwaysInterleave]
        Task<TUser> Get();

        /// <summary>
        /// Get the claims associated with this user
        /// </summary>
        /// <returns>A list of claims</returns>
        Task<IList<IdentityUserClaim<Guid>>> GetClaims();

        /// <summary>
        /// Gets the login for the current usr
        /// </summary>
        /// <param name="loginProvider">The login provider</param>
        /// <param name="providerKey">The login key</param>
        /// <returns>The loging</returns>
        [AlwaysInterleave]
        Task<IdentityUserLogin<Guid>> GetLogin(string loginProvider, string providerKey);

        /// <summary>
        /// Gets all the logins for the current user
        /// </summary>
        /// <returns>A list of logins</returns>
        [AlwaysInterleave]
        Task<IList<IdentityUserLogin<Guid>>> GetLogins();

        /// <summary>
        /// Gets the roles for the current user
        /// </summary>
        /// <returns>A list of role names</returns>
        [AlwaysInterleave]
        Task<IList<string>> GetRoles();

        /// <summary>
        /// Gets a token
        /// </summary>
        /// <param name="loginProvider">The login provider</param>
        /// <param name="name">The name</param>
        /// <returns>The user token</returns>
        [AlwaysInterleave]
        Task<IdentityUserToken<Guid>> GetToken(string loginProvider, string name);

        /// <summary>
        /// Remove claims for current user
        /// </summary>
        /// <param name="claims">A list of claims to remove</param>
        Task RemoveClaims(IList<Claim> claims);

        /// <summary>
        /// Removes a login from the current user
        /// </summary>
        /// <param name="loginProvider">The login provider</param>
        /// <param name="providerKey">The login key</param>
        Task RemoveLogin(string loginProvider, string providerKey);

        /// <summary>
        /// Removes a role from the current user
        /// </summary>
        /// <param name="id">The ID of the role to remove</param>
        /// <param name="updateRoleGrain">if true, will remove user from role grain</param>
        Task RemoveRole(Guid id, bool updateRoleGrain);

        /// <summary>
        /// Removes token from user
        /// </summary>
        /// <param name="token">the token to remove</param>
        Task RemoveToken(IdentityUserToken<Guid> token);

        /// <summary>
        /// Replaces claims for the current user
        /// </summary>
        /// <param name="claim">The claim to replace</param>
        /// <param name="newClaim">The new claim</param>
        Task ReplaceClaims(Claim claim, Claim newClaim);

        /// <summary>
        /// Updates the current user
        /// </summary>
        /// <param name="user">The updated user</param>
        /// <returns>Result of the operations</returns>
        Task<IdentityResult> Update(TUser user);
    }
}