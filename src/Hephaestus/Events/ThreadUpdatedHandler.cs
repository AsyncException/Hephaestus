using Discord;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("ThreadUpdated", GatewayIntents.None)]
public abstract class ThreadUpdatedHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected ThreadUpdatedParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (ThreadUpdatedParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {

        client.ThreadUpdated += (arg1, arg2) => execution(client, services, key, new ThreadUpdatedParameters(arg1, arg2));
    }

}

public record ThreadUpdatedParameters(Cacheable<SocketThreadChannel, ulong> OldSocketThreadChannel, SocketThreadChannel SocketThreadChannel) : IEventParameters;