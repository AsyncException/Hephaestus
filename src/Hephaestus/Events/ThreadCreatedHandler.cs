using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("ThreadCreated", GatewayIntents.None)]
public abstract class ThreadCreatedHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected ThreadCreatedParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (ThreadCreatedParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.ThreadCreated += (SocketThreadChannel) => execution(new ThreadCreatedParameters(SocketThreadChannel));
}

public record ThreadCreatedParameters(SocketThreadChannel SocketThreadChannel) : IEventParameters;