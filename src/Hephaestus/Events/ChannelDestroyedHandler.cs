using Discord;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("ChannelDestroyed", GatewayIntents.Guilds)]
public abstract class ChannelDestroyedHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected ChannelDestroyedParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (ChannelDestroyedParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {
        client.ChannelDestroyed += (arg1) => execution(client, services, key, new ChannelDestroyedParameters(arg1));
    }
}

public record ChannelDestroyedParameters(SocketChannel SocketChannel) : IEventParameters;