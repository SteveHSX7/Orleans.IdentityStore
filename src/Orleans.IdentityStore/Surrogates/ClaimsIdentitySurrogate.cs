using System.Collections.Generic;
using System.Security.Claims;

namespace Orleans.IdentityStore.Surrogates;

// This is a surrogate for the Claim class
// https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/serialization?pivots=orleans-7-0#surrogates-for-serializing-foreign-types
// Slightly tricky one to surrogate due to the properties not necessarilt reflecting how
// the object is constructed.
// This is based on https://github.com/codekoenig/AspNetCore.Identity.DocumentDb/blob/master/src/AspNetCore.Identity.DocumentDb/Tools/JsonClaimsIdentityConverter.cs
[GenerateSerializer, Immutable]
public readonly record struct ClaimsIdentitySurrogate(string nameClaimType, 
                                                      string roleClaimType, 
                                                      IEnumerable<Claim> claims, 
                                                      string authenticationType);


// This is a converter that converts between the surrogate and the foreign type.
[RegisterConverter]
public sealed class ClaimsIdentityConverter : IConverter<ClaimsIdentity, ClaimsIdentitySurrogate> 
{
    public ClaimsIdentity ConvertFromSurrogate(
        in ClaimsIdentitySurrogate surrogate) 
            => new ClaimsIdentity(surrogate.claims, surrogate.authenticationType, surrogate.nameClaimType, surrogate.roleClaimType);

    public ClaimsIdentitySurrogate ConvertToSurrogate(
        in ClaimsIdentity value) =>
        new() { 
            authenticationType = value.AuthenticationType,
            nameClaimType = value.NameClaimType,
            roleClaimType = value.RoleClaimType,
            claims = value.Claims,
        };
}