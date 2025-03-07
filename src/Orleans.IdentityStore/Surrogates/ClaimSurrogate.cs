using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace Orleans.IdentityStore.Surrogates;

// This is a surrogate for the Claim class
// https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/serialization?pivots=orleans-7-0#surrogates-for-serializing-foreign-types
[GenerateSerializer, Immutable]
public readonly record struct ClaimSurrogate(string type, string value, string valueType, string issuer, string originalIssuer, ClaimsIdentity claimsIdentity);

// This is a converter that converts between the surrogate and the foreign type.
[RegisterConverter]
public sealed class ClaimConverter : IConverter<Claim, ClaimSurrogate>
{
    public Claim ConvertFromSurrogate(
        in ClaimSurrogate surrogate) =>
        new(surrogate.type, surrogate.value, surrogate.valueType, surrogate.issuer, surrogate.originalIssuer, surrogate.claimsIdentity);

    public ClaimSurrogate ConvertToSurrogate(
        in Claim value) =>
        new(value.Type, value.Value, value.ValueType, value.Issuer, value.OriginalIssuer, value.Subject);
}