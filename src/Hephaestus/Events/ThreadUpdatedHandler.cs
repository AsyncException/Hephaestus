using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("ThreadUpdated", GatewayIntents.None)]
public abstract class ThreadUpdatedHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected ThreadUpdatedParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (ThreadUpdatedParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.ThreadUpdated += (OldSocketThreadChannel, SocketThreadChannel) => execution(new ThreadUpdatedParameters(OldSocketThreadChannel, SocketThreadChannel));
}

public record ThreadUpdatedParameters(Cacheable<SocketThreadChannel, ulong> OldSocketThreadChannel, SocketThreadChannel SocketThreadChannel) : IEventParameters;