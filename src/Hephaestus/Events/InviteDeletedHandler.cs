using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("InviteDeleted", GatewayIntents.GuildInvites)]
public abstract class InviteDeletedHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected InviteDeletedParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (InviteDeletedParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.InviteDeleted += (SocketGuildChannel, DeletedInviteCode) => execution(new InviteDeletedParameters(SocketGuildChannel, DeletedInviteCode));
}

public record InviteDeletedParameters(SocketGuildChannel SocketGuildChannel, string DeletedInviteCode) : IEventParameters;