using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("GuildScheduledEventCreated", GatewayIntents.GuildScheduledEvents)]
public abstract class GuildScheduledEventCreatedHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected GuildScheduledEventCreatedParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (GuildScheduledEventCreatedParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.GuildScheduledEventCreated += (SocketGuildEvent) => execution(new GuildScheduledEventCreatedParameters(SocketGuildEvent));
}

public record GuildScheduledEventCreatedParameters(SocketGuildEvent SocketGuildEvent) : IEventParameters;