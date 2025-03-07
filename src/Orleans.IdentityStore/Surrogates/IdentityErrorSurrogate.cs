using Microsoft.AspNetCore.Identity;

namespace Orleans.IdentityStore.Surrogates;

// This is a surrogate for the IdentityError class
// https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/serialization?pivots=orleans-7-0#surrogates-for-serializing-foreign-types
[GenerateSerializer, Immutable]
public readonly record struct IdentityErrorSurrogate(string Code, string Description);

// This is a converter that converts between the surrogate and the foreign type.
[RegisterConverter]
public sealed class IdentityErrorConverter : IConverter<IdentityError, IdentityErrorSurrogate>
{
    public IdentityError ConvertFromSurrogate(
        in IdentityErrorSurrogate surrogate) =>
        new() {
            Code = surrogate.Code, 
            Description = surrogate.Description
        };

    public IdentityErrorSurrogate ConvertToSurrogate(
        in IdentityError value) =>
        new(value.Code, value.Description);
}
