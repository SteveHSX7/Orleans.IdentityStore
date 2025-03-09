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
        new(type: surrogate.type, value: surrogate.value, valueType: surrogate.valueType, issuer: surrogate.issuer, originalIssuer: surrogate.originalIssuer, subject: surrogate.claimsIdentity);

    public ClaimSurrogate ConvertToSurrogate(
        in Claim value) =>
        new(type: value.Type, value: value.Value, valueType: value.ValueType, issuer: value.Issuer, originalIssuer: value.OriginalIssuer, claimsIdentity: value.Subject);
}