using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("GuildScheduledEventStarted", GatewayIntents.None)]
public abstract class GuildScheduledEventStartedHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected GuildScheduledEventStartedParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (GuildScheduledEventStartedParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.GuildScheduledEventStarted += (SocketGuildEvent) => execution(new GuildScheduledEventStartedParameters(SocketGuildEvent));
}

public record GuildScheduledEventStartedParameters(SocketGuildEvent SocketGuildEvent) : IEventParameters;