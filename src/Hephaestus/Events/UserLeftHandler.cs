using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("UserLeft", GatewayIntents.Guilds)]
public abstract class UserLeftHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected UserLeftParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (UserLeftParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.UserLeft += (SocketGuild, SocketUser) => execution(new UserLeftParameters(SocketGuild, SocketUser));
}

public record UserLeftParameters(SocketGuild SocketGuild, SocketUser SocketUser) : IEventParameters;