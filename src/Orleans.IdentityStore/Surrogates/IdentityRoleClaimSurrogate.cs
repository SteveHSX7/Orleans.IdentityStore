using System;
using Microsoft.AspNetCore.Identity;

namespace Orleans.IdentityStore.Surrogates;

// This is a surrogate for the IdentityRoleClaim<TKey> class
// https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/serialization?pivots=orleans-7-0#surrogates-for-serializing-foreign-types
[GenerateSerializer, Immutable]
public readonly record struct IdentityRoleClaimSurrogate<T>(int Id, T RoleId, string ClaimType, string ClaimValue) where T : IEquatable<T>;

// This is a converter that converts between the surrogate and the foreign type.
[RegisterConverter]
public sealed class IdentityRoleClaimConverter<T> : IConverter<IdentityRoleClaim<T>, IdentityRoleClaimSurrogate<T>> where T : IEquatable<T>
{
    public IdentityRoleClaim<T> ConvertFromSurrogate(
        in IdentityRoleClaimSurrogate<T> surrogate) =>
        new() { 
            Id = surrogate.Id,
            RoleId = surrogate.RoleId, 
            ClaimType = surrogate.ClaimType, 
            ClaimValue = surrogate.ClaimValue
        };

    public IdentityRoleClaimSurrogate<T> ConvertToSurrogate(
        in IdentityRoleClaim<T> value) =>
        new()
        {
            Id = value.Id,
            RoleId = value.RoleId,
            ClaimType = value.ClaimType,
            ClaimValue = value.ClaimValue
        };
}