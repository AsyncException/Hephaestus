using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("MessageReceived", GatewayIntents.GuildMessages, GatewayIntents.MessageContent)]
public abstract class MessageReceivedHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected MessageReceivedParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (MessageReceivedParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.MessageReceived += (SocketMessage) => execution(new MessageReceivedParameters(SocketMessage));
}

public record MessageReceivedParameters(SocketMessage SocketMessage) : IEventParameters;