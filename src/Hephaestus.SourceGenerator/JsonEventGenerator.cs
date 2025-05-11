using Hephaestus.SourceGenerator.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using Scriban;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using Hephaestus.SourceGenerator.Templates;
using System.Threading;

namespace Hephaestus.SourceGenerator;

[Generator]
public class JsonEventGenerator : IIncrementalGenerator
{
    private static readonly Template template = Template.Parse(TemplateProvider.JsonEvent);

    public void Initialize(IncrementalGeneratorInitializationContext context) => 
        context.RegisterSourceOutput(context.AdditionalTextsProvider.Where(IsValid).SelectMany(Transform), GenerateSourceOutput);
    private static bool IsValid(AdditionalText text) => 
        text.Path.EndsWith("DiscordSocketEvents.json");

    private static IEnumerable<ScribanEventData> Transform(AdditionalText text, CancellationToken token) => 
        JsonSerializer.Deserialize(text.GetText(token).ToString(), EventDataJsonContext.Default.ListJsonEventData).Select(ScribanConverter.Convert);

    private static void GenerateSourceOutput(SourceProductionContext spc, ScribanEventData data) =>
            spc.AddSource($"{data.Name}Handler.g.cs", SourceText.From(template.Render(data), Encoding.UTF8));
}