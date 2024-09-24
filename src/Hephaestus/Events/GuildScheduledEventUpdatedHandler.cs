using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("GuildScheduledEventUpdated", GatewayIntents.GuildScheduledEvents)]
public abstract class GuildScheduledEventUpdatedHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected GuildScheduledEventUpdatedParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (GuildScheduledEventUpdatedParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.GuildScheduledEventUpdated += (OldSocketGuildEvent, SocketGuildEvent) => execution(new GuildScheduledEventUpdatedParameters(OldSocketGuildEvent, SocketGuildEvent));
}

public record GuildScheduledEventUpdatedParameters(Cacheable<SocketGuildEvent, ulong> OldSocketGuildEvent, SocketGuildEvent SocketGuildEvent) : IEventParameters;