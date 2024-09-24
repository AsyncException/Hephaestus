using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("GuildScheduledEventUserRemove", GatewayIntents.GuildScheduledEvents)]
public abstract class GuildScheduledEventUserRemoveHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected GuildScheduledEventUserRemoveParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (GuildScheduledEventUserRemoveParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.GuildScheduledEventUserRemove += (User, SocketGuildEvent) => execution(new GuildScheduledEventUserRemoveParameters(User, SocketGuildEvent));
}

public record GuildScheduledEventUserRemoveParameters(Cacheable<SocketUser, RestUser, IUser, ulong> User, SocketGuildEvent SocketGuildEvent) : IEventParameters;