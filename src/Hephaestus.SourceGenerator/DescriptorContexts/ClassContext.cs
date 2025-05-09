using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

namespace Hephaestus.SourceGenerator.DescriptorContexts;

public record ClassContext(ClassDeclarationSyntax ClassSyntax, INamedTypeSymbol Symbol) {
    public string SanitizedClassName => Symbol.Name;
    public string ClassName => Symbol.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat);
    public string Namespace => Symbol.ContainingNamespace.ToDisplayString();
    public string[] GenericParameters => Symbol.TypeArguments.Select(e => e.ToDisplayString()).ToArray();
    public bool HasAttribute(string attributeName) => Symbol.GetAttributes().Any(attr => attr.AttributeClass.ToDisplayString() == attributeName);
    public AttributeData GetAttribute(string attributeName) => Symbol.GetAttributes().Where(attr => attr.AttributeClass.ToDisplayString() == attributeName).First();
}
