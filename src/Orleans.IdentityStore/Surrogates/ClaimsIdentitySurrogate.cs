using System.Security.Claims;

namespace Orleans.IdentityStore.Surrogates;

// This is a surrogate for the Claim class
// https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/serialization?pivots=orleans-7-0#surrogates-for-serializing-foreign-types
[GenerateSerializer, Immutable]
public readonly record struct ClaimsIdentitySurrogate(string label, ClaimsIdentity actor);


// This is a converter that converts between the surrogate and the foreign type.
[RegisterConverter]
public sealed class ClaimsIdentityConverter : IConverter<ClaimsIdentity, ClaimsIdentitySurrogate> 
{
    public ClaimsIdentity ConvertFromSurrogate(
        in ClaimsIdentitySurrogate surrogate) =>
        
        new() { 
            Label = surrogate.label,
            Actor = surrogate.actor
        };

    public ClaimsIdentitySurrogate ConvertToSurrogate(
        in ClaimsIdentity value) =>
        new() { 
            label = value.Label,
            actor = value.Actor
        };
}