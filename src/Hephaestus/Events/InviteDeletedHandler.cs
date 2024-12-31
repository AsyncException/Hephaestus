using Discord;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("InviteDeleted", GatewayIntents.GuildInvites)]
public abstract class InviteDeletedHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected InviteDeletedParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (InviteDeletedParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {

        client.InviteDeleted += (arg1, arg2) => execution(client, services, key, new InviteDeletedParameters(arg1, arg2));
    }

}

public record InviteDeletedParameters(SocketGuildChannel SocketGuildChannel, string DeletedInviteCode) : IEventParameters;