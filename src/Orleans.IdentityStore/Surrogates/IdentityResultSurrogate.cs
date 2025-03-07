using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace Orleans.IdentityStore.Surrogates;

// This is a surrogate for the IdentityResults class
// https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/serialization?pivots=orleans-7-0#surrogates-for-serializing-foreign-types
[GenerateSerializer, Immutable]
public readonly record struct IdentityResultSurrogate(bool Succeeded, IEnumerable<IdentityError> Errors);

// This is a converter that converts between the surrogate and the foreign type.
[RegisterConverter]
public sealed class IdentityResultConverter : IConverter<IdentityResult, IdentityResultSurrogate>
{
    public IdentityResult ConvertFromSurrogate(
        in IdentityResultSurrogate surrogate) =>
        surrogate.Succeeded ? IdentityResult.Success : IdentityResult.Failed(surrogate.Errors.ToArray());

    public IdentityResultSurrogate ConvertToSurrogate(
        in IdentityResult value) =>
        new(value.Succeeded, value.Errors.ToArray());
}