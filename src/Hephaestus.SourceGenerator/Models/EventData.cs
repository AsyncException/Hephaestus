using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Hephaestus.SourceGenerator.Models;

public record JsonEventData([property: JsonPropertyName("name")] string Name, [property: JsonPropertyName("parameters")] string[] Parameters, [property: JsonPropertyName("intents")] string[] Intents);
public record ScribanEventData(string Name, string Parameters, string Intents);

public static partial class ScribanConverter {
    public static ScribanEventData Convert(JsonEventData data) => 
        new(data.Name, string.Join(", ", data.Parameters.Select((arg, index) => $"{arg} arg{index}")), string.Join(", ", data.Intents.Select(intent => $"GatewayIntents.{intent}")));
}

[JsonSerializable(typeof(List<JsonEventData>))]
internal partial class EventDataJsonContext : JsonSerializerContext;