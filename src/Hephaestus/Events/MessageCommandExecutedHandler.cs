using Discord;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("MessageCommandExecuted", GatewayIntents.None)]
public abstract class MessageCommandExecutedHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected MessageCommandExecutedParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (MessageCommandExecutedParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {

        client.MessageCommandExecuted += (arg1) => execution(client, services, key, new MessageCommandExecutedParameters(arg1));
    }

}

public record MessageCommandExecutedParameters(SocketMessageCommand SocketMessageCommand) : IEventParameters;