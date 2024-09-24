using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("Log", GatewayIntents.None)]
public abstract class LogHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected LogParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (LogParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.Log += (LogMessage) => execution(new LogParameters(LogMessage));
}

public record LogParameters(LogMessage LogMessage) : IEventParameters;