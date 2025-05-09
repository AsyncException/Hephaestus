using Hephaestus.SourceGenerator.DescriptorContexts;
using Hephaestus.SourceGenerator.ExtentionWrappers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Linq;
using System.Text;

namespace Hephaestus.SourceGenerator;

[Generator]
public class InternalEventHandlerGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context) {
        IncrementalValuesProvider<ClassContext> eventHandlers = context.SyntaxProvider
            .CreateSyntaxProvider(
                predicate: static (syntaxNode, _) => syntaxNode is ClassDeclarationSyntax classSyntax && classSyntax.AttributeLists.Count > 0,
                transform: static (syntaxNode, _) => {
                    ClassContext context = new((ClassDeclarationSyntax)syntaxNode.Node, syntaxNode.SemanticModel.GetDeclaredSymbol((ClassDeclarationSyntax)syntaxNode.Node));
                    return context.HasAttribute("Hephaestus.Events.EventHandlerAttribute") ? context : null;
                })
            .Where(static x => x is not null);

        context.RegisterSourceOutput(eventHandlers, RegisterEventHandlerSource);
    }

    public static void RegisterEventHandlerSource(SourceProductionContext spc, ClassContext context) {
        try {
            if(!ValidateClassIsGeneric(spc, context)) {
                
                return;
            }

            EventHandlerParameters parameters = new(context.GetAttribute("Hephaestus.Events.EventHandlerAttribute")); // will never be null

            string source = $$"""
            namespace {{context.Namespace}};

            public abstract partial class {{context.ClassName}} : IEventHandler<{{context.GenericParameters[0]}}> where {{context.GenericParameters[0]}} : {{context.ClassName}} {

                public abstract Task Execute({{parameters.GetParameters()}});
                
                static Discord.GatewayIntents[] Hephaestus.Events.IEventHandler<{{context.GenericParameters[0]}}>.RequiredIntents { get; } = [{{parameters.GetGatewayIntents()}}];
                
                static void Hephaestus.Events.IEventHandler<{{context.GenericParameters[0]}}>.RegisterToClient(Discord.WebSocket.DiscordSocketClient client, System.IServiceProvider services) {
                    client.{{context.SanitizedClassName.Replace("Handler", "")}} += async ({{string.Join(", ", Enumerable.Range(1, parameters.ParameterTypes.Length).Select(e => $"arg{e}"))}}) => {
                        {{context.GenericParameters[0]}} handler = Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService<{{context.GenericParameters[0]}}>(services);
                        await handler.Execute({{string.Join(", ", Enumerable.Range(1, parameters.ParameterTypes.Length).Select(e => $"arg{e}"))}});
                    };
                }
            }
            """;

            string hintName = $"{context.SanitizedClassName}_EventHandler.g.cs";
            spc.AddSource(hintName, SourceText.From(source, Encoding.UTF8));
        }
        catch (Exception ex) {
            Diagnostic diagnostic = Diagnostic.Create(new DiagnosticDescriptor(id: "SG0001", title: "Exception", messageFormat: "{0}: {1}", category: "InternalEventHandlerGenerator", DiagnosticSeverity.Warning, isEnabledByDefault: true), context.ClassSyntax.GetLocation(), ex.Message, ex.StackTrace);
            spc.ReportDiagnostic(diagnostic);
        }
    }

    private static bool ValidateClassIsGeneric(SourceProductionContext spc, ClassContext context) {
        if (context.ClassSyntax.TypeParameterList.Parameters.Count == 0) {
            DiagnosticDescriptor descriptor = new(id: "GEN001", title: "Class must be generic", messageFormat: "The class '{0}' must be a generic type.", category: "SourceGenerator", DiagnosticSeverity.Warning, isEnabledByDefault: true);
            spc.ReportDiagnostic(Diagnostic.Create(descriptor,context.ClassSyntax.GetLocation(),context.SanitizedClassName));
            return false;
        }

        return true;
    }
}
