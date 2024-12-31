using Discord;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("MessageReceived", GatewayIntents.GuildMessages, GatewayIntents.MessageContent)]
public abstract class MessageReceivedHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected MessageReceivedParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (MessageReceivedParameters)parameters;
    }


    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {
        client.MessageReceived += async (arg1) => await execution(client, services, key, new MessageReceivedParameters(arg1));
    }
}

public record MessageReceivedParameters(SocketMessage SocketMessage) : IEventParameters;