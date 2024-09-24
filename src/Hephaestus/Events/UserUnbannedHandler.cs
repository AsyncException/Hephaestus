using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("UserUnbanned", GatewayIntents.GuildBans)]
public abstract class UserUnbannedHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected UserUnbannedParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (UserUnbannedParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.UserUnbanned += (SocketUser, SocketGuild) => execution(new UserUnbannedParameters(SocketUser, SocketGuild));
}

public record UserUnbannedParameters(SocketUser SocketUser, SocketGuild SocketGuild) : IEventParameters;