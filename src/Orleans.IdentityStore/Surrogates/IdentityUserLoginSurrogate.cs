using System;
using Microsoft.AspNetCore.Identity;

namespace Orleans.IdentityStore.Surrogates;

// This is a surrogate for the IdentityUserLogin<TKey> class
// https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/serialization?pivots=orleans-7-0#surrogates-for-serializing-foreign-types
[GenerateSerializer, Immutable]
public readonly record struct IdentityUserLoginSurrogate<T>(string LoginProvider, string ProviderKey, string ProviderDisplayName, T UserId) where T : IEquatable<T>;
// This is a converter that converts between the surrogate and the foreign type.
[RegisterConverter]
public sealed class IdentityUserLoginConverter<T> : IConverter<IdentityUserLogin<T>, IdentityUserLoginSurrogate<T>> where T : IEquatable<T>
{
    public IdentityUserLogin<T> ConvertFromSurrogate(
        in IdentityUserLoginSurrogate<T> surrogate) =>
        new() { 
            LoginProvider = surrogate.LoginProvider,
            ProviderKey = surrogate.ProviderKey, 
            ProviderDisplayName = surrogate.ProviderDisplayName, 
            UserId = surrogate.UserId
        };

    public IdentityUserLoginSurrogate<T> ConvertToSurrogate(
        in IdentityUserLogin<T> value) =>
        new()
        {
            LoginProvider = value.LoginProvider,
            ProviderKey = value.ProviderKey,
            ProviderDisplayName = value.ProviderDisplayName,
            UserId = value.UserId
        };
}