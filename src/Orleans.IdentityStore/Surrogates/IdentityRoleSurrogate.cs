using System;
using Microsoft.AspNetCore.Identity;

namespace Orleans.IdentityStore.Surrogates;

// This is a surrogate for the IdentityRole<TKey> class
// https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/serialization?pivots=orleans-7-0#surrogates-for-serializing-foreign-types
[GenerateSerializer, Immutable]
public readonly record struct IdentityRoleSurrogate<T>(T Id, string Name, string NormalizedName, string ConcurrencyStamp) where T : IEquatable<T>;

// This is a converter that converts between the surrogate and the foreign type.
[RegisterConverter]
public sealed class IdentityRoleConverter<T> : IConverter<IdentityRole<T>, IdentityRoleSurrogate<T>> where T : IEquatable<T>
{
    public IdentityRole<T> ConvertFromSurrogate(
        in IdentityRoleSurrogate<T> surrogate) =>
        new()
        {
            Id = surrogate.Id,
            Name = surrogate.Name,
            NormalizedName = surrogate.NormalizedName,
            ConcurrencyStamp = surrogate.ConcurrencyStamp
        };

    public IdentityRoleSurrogate<T> ConvertToSurrogate(
        in IdentityRole<T> value) =>
        new()
        {
            Id = value.Id,
            Name = value.Name,
            NormalizedName = value.NormalizedName,
            ConcurrencyStamp = value.ConcurrencyStamp
        };
}