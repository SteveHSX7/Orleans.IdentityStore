using System;
using Microsoft.AspNetCore.Identity;

namespace Orleans.IdentityStore.Surrogates;

// This is a surrogate for the IdentityUser class
// https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/serialization?pivots=orleans-7-0#surrogates-for-serializing-foreign-types
[GenerateSerializer, Immutable]
public readonly record struct IdentityUserSurrogate<T>(T Id, string UserName, string NormalizedUserName, string Email, string NormalizedEmail, bool EmailConfirmed, string PasswordHash, string PhoneNumber, bool PhoneNumberConfirmed, bool TwoFactorEnabled, string ConcurrencyStamp) where T : IEquatable<T>;

// This is a converter that converts between the surrogate and the foreign type.
[RegisterConverter]
public sealed class IdentityUserConverter<T> : IConverter<IdentityUser<T>, IdentityUserSurrogate<T>> where T : IEquatable<T>
{
    public IdentityUser<T> ConvertFromSurrogate(
        in IdentityUserSurrogate<T> surrogate) =>
        new()
        {
            Id = surrogate.Id,
            UserName = surrogate.UserName,
            NormalizedUserName = surrogate.NormalizedUserName,
            Email = surrogate.Email,
            NormalizedEmail = surrogate.NormalizedEmail,
            EmailConfirmed = surrogate.EmailConfirmed,
            PasswordHash = surrogate.PasswordHash,
            PhoneNumber = surrogate.PhoneNumber,
            PhoneNumberConfirmed = surrogate.PhoneNumberConfirmed,
            TwoFactorEnabled = surrogate.TwoFactorEnabled,
            ConcurrencyStamp = surrogate.ConcurrencyStamp
        };

    public IdentityUserSurrogate<T> ConvertToSurrogate(
        in IdentityUser<T> value) =>
        new()
        {
            Id = value.Id,
            UserName = value.UserName,
            NormalizedUserName = value.NormalizedUserName,
            Email = value.Email,
            NormalizedEmail = value.NormalizedEmail,
            EmailConfirmed = value.EmailConfirmed,
            PasswordHash = value.PasswordHash,
            PhoneNumber = value.PhoneNumber,
            PhoneNumberConfirmed = value.PhoneNumberConfirmed,
            TwoFactorEnabled = value.TwoFactorEnabled,
            ConcurrencyStamp = value.ConcurrencyStamp
        };
}