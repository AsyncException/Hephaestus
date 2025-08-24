using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Collections.Immutable;
using System.Linq;

namespace Hephaestus.SourceGenerator.ExtentionWrappers;

public record EventHandlerParameters(AttributeData Data) {
    public ImmutableArray<string> ParameterTypes { get; } = Data.ConstructorArguments[0].Values.Select(e => e.Value.ToString()).ToImmutableArray();
    public string GetParameters() => string.Join(",", ParameterTypes.Select((type, index) => $"{type} arg{index}"));
    public string GetGatewayIntents() => string.Join(",", Data.ConstructorArguments[1].Values.Select(e => e.ToCSharpString()));
}