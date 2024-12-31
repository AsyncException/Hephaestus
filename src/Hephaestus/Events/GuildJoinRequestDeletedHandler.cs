using Discord;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("GuildJoinRequestDeleted", GatewayIntents.None)]
public abstract class GuildJoinRequestDeletedHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected GuildJoinRequestDeletedParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (GuildJoinRequestDeletedParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {

        client.GuildJoinRequestDeleted += (arg1, arg2) => execution(client, services, key, new GuildJoinRequestDeletedParameters(arg1, arg2));
    }

}

public record GuildJoinRequestDeletedParameters(Cacheable<SocketGuildUser, ulong> SocketGuildUser, SocketGuild SocketGuild) : IEventParameters;