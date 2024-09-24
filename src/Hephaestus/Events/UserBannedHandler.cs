using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("UserBanned", GatewayIntents.GuildBans)]
public abstract class UserBannedHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected UserBannedParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (UserBannedParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.UserBanned += (SocketUser, SocketGuild) => execution(new UserBannedParameters(SocketUser, SocketGuild));
}

public record UserBannedParameters(SocketUser SocketUser, SocketGuild SocketGuild) : IEventParameters;