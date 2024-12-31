using Discord;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("ThreadDeleted", GatewayIntents.None)]
public abstract class ThreadDeletedHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected ThreadDeletedParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (ThreadDeletedParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {

        client.ThreadDeleted += (arg1) => execution(client, services, key, new ThreadDeletedParameters(arg1));
    }

}

public record ThreadDeletedParameters(Cacheable<SocketThreadChannel, ulong> SocketThreadChannel) : IEventParameters;