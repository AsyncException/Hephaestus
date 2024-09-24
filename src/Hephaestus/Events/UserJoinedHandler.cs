using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("UserJoined", GatewayIntents.GuildMembers)]
public abstract class UserJoinedHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected UserJoinedParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (UserJoinedParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.UserJoined += (SocketUser) => execution(new UserJoinedParameters(SocketUser));
}

public record UserJoinedParameters(SocketUser SocketUser) : IEventParameters;