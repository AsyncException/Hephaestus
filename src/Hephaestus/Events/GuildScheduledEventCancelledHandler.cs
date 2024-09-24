using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("GuildScheduledEventCancelled", GatewayIntents.GuildScheduledEvents)]
public abstract class GuildScheduledEventCancelledHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected GuildScheduledEventCancelledParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (GuildScheduledEventCancelledParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.GuildScheduledEventCancelled += (SocketGuildEvent) => execution(new GuildScheduledEventCancelledParameters(SocketGuildEvent));
}

public record GuildScheduledEventCancelledParameters(SocketGuildEvent SocketGuildEvent) : IEventParameters;