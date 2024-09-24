using Discord;
using Discord.WebSocket;

namespace Hephaestus.EventHandling;

public interface IEventHandler
{
    public void PrepareContext(DiscordSocketClient client, IEventParameters Parameters);

    public Task Execute();
}

public interface IEventParameters;

public abstract class EventHandler : IEventHandler
{
    public abstract void PrepareContext(DiscordSocketClient client, IEventParameters Parameters);

    public abstract Task Execute();
}

[AttributeUsage(AttributeTargets.Class)]
public class EventHandlerAttribute(string event_type, params GatewayIntents[] intent) : Attribute
{
    public string EventType { get; init; } = event_type;
    public GatewayIntents[] Intent { get; init; } = intent;
}