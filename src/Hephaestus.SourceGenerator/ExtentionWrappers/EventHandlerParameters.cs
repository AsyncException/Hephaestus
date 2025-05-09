using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Collections.Immutable;
using System.Linq;

namespace Hephaestus.SourceGenerator.ExtentionWrappers;

public record EventHandlerParameters(AttributeData Data) {
    public ImmutableArray<string> ParameterNames { get; } = Data.ConstructorArguments[0].Values.Select(e => (string)e.Value).ToImmutableArray();
    public ImmutableArray<string> ParameterTypes { get; } = Data.ConstructorArguments[1].Values.Select(e => e.Value.ToString()).ToImmutableArray();
    public string GetParameters() => string.Join(",", ParameterNames.Zip(ParameterTypes, (name, type) => $"{type} {name}"));
    public string GetGatewayIntents() => string.Join(",", Data.ConstructorArguments[2].Values.Select(e => e.ToCSharpString()));
}