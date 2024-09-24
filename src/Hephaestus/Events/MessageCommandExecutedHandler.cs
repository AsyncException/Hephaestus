using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("MessageCommandExecuted", GatewayIntents.None)]
public abstract class MessageCommandExecutedHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected MessageCommandExecutedParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (MessageCommandExecutedParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.MessageCommandExecuted += (SocketMessageCommand) => execution(new MessageCommandExecutedParameters(SocketMessageCommand));
}

public record MessageCommandExecutedParameters(SocketMessageCommand SocketMessageCommand) : IEventParameters;