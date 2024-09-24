using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("GuildScheduledEventUserAdd", GatewayIntents.GuildScheduledEvents)]
public abstract class GuildScheduledEventUserAddHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected GuildScheduledEventUserAddParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (GuildScheduledEventUserAddParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.GuildScheduledEventUserAdd += (User, SocketGuildEvent) => execution(new GuildScheduledEventUserAddParameters(User, SocketGuildEvent));
}

public record GuildScheduledEventUserAddParameters(Cacheable<SocketUser, RestUser, IUser, ulong> User, SocketGuildEvent SocketGuildEvent) : IEventParameters;