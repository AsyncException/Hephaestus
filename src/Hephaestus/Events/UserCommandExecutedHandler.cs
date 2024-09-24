using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("UserCommandExecuted", GatewayIntents.None)]
public abstract class UserCommandExecutedHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected UserCommandExecutedParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (UserCommandExecutedParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.UserCommandExecuted += (SocketUserCommand) => execution(new UserCommandExecutedParameters(SocketUserCommand));
}

public record UserCommandExecutedParameters(SocketUserCommand SocketUserCommand) : IEventParameters;