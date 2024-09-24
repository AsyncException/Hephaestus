using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("UserUpdated", GatewayIntents.Guilds)]
public abstract class UserUpdatedHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected UserUpdatedParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (UserUpdatedParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.UserUpdated += (OldSocketUser, SocketUser) => execution(new UserUpdatedParameters(OldSocketUser, SocketUser));
}

public record UserUpdatedParameters(SocketUser OldSocketUser, SocketUser SocketUser) : IEventParameters;