using System;
using Microsoft.AspNetCore.Identity;

namespace Orleans.IdentityStore.Surrogates;

// This is a surrogate for the IdentityUserToken<TKey> class
// https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/serialization?pivots=orleans-7-0#surrogates-for-serializing-foreign-types
[GenerateSerializer, Immutable]
public readonly record struct IdentityUserTokenSurrogate<T>(string LoginProvider, string Name, string Value, T UserId) where T : IEquatable<T>;
// This is a converter that converts between the surrogate and the foreign type.
[RegisterConverter]
public sealed class IdentityUserTokenConverter<T> : IConverter<IdentityUserToken<T>, IdentityUserTokenSurrogate<T>> where T : IEquatable<T>
{
    public IdentityUserToken<T> ConvertFromSurrogate(
        in IdentityUserTokenSurrogate<T> surrogate) =>
        new() { 
            LoginProvider = surrogate.LoginProvider,
            Name = surrogate.Name, 
            Value = surrogate.Value, 
            UserId = surrogate.UserId
        };

    public IdentityUserTokenSurrogate<T> ConvertToSurrogate(
        in IdentityUserToken<T> value) =>
        new()
        {
            LoginProvider = value.LoginProvider,
            Name = value.Name,
            Value = value.Value,
            UserId = value.UserId
        };
}