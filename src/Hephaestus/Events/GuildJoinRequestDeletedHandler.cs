using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("GuildJoinRequestDeleted", GatewayIntents.None)]
public abstract class GuildJoinRequestDeletedHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected GuildJoinRequestDeletedParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (GuildJoinRequestDeletedParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.GuildJoinRequestDeleted += (SocketGuildUser, SocketGuild) => execution(new GuildJoinRequestDeletedParameters(SocketGuildUser, SocketGuild));
}

public record GuildJoinRequestDeletedParameters(Cacheable<SocketGuildUser, ulong> SocketGuildUser, SocketGuild SocketGuild) : IEventParameters;