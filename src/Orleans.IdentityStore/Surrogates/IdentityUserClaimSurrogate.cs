using System;
using Microsoft.AspNetCore.Identity;

namespace Orleans.IdentityStore.Surrogates;

// This is a surrogate for the IdentityUserClaim<TKey> class
// https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/serialization?pivots=orleans-7-0#surrogates-for-serializing-foreign-types
[GenerateSerializer, Immutable]
public readonly record struct IdentityUserClaimSurrogate<T>(int Id, T UserId, string ClaimType, string ClaimValue) where T : IEquatable<T>;

// This is a converter that converts between the surrogate and the foreign type.
[RegisterConverter]
public sealed class IdentityUserClaimConverter<T> : IConverter<IdentityUserClaim<T>, IdentityUserClaimSurrogate<T>> where T : IEquatable<T>
{
    public IdentityUserClaim<T> ConvertFromSurrogate(
        in IdentityUserClaimSurrogate<T> surrogate) =>
        new() { 
            Id = surrogate.Id,
            UserId = surrogate.UserId, 
            ClaimType = surrogate.ClaimType, 
            ClaimValue = surrogate.ClaimValue
        };

    public IdentityUserClaimSurrogate<T> ConvertToSurrogate(
        in IdentityUserClaim<T> value) =>
        new()
        {
            Id = value.Id,
            UserId = value.UserId,
            ClaimType = value.ClaimType,
            ClaimValue = value.ClaimValue
        };
}