using Discord;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("UserCommandExecuted", GatewayIntents.None)]
public abstract class UserCommandExecutedHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected UserCommandExecutedParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (UserCommandExecutedParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {

        client.UserCommandExecuted += (arg1) => execution(client, services, key, new UserCommandExecutedParameters(arg1));
    }

}

public record UserCommandExecutedParameters(SocketUserCommand SocketUserCommand) : IEventParameters;