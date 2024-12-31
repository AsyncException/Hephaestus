using Discord;

namespace Hephaestus.Events.EventHandling;

[AttributeUsage(AttributeTargets.Class)]
public class EventHandlerAttribute(string event_type, params GatewayIntents[] intent) : Attribute
{
    public string EventType { get; init; } = event_type;
    public GatewayIntents[] Intent { get; init; } = intent;
}