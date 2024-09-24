using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("ThreadDeleted", GatewayIntents.None)]
public abstract class ThreadDeletedHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected ThreadDeletedParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (ThreadDeletedParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.ThreadDeleted += (SocketThreadChannel) => execution(new ThreadDeletedParameters(SocketThreadChannel));
}

public record ThreadDeletedParameters(Cacheable<SocketThreadChannel, ulong> SocketThreadChannel) : IEventParameters;