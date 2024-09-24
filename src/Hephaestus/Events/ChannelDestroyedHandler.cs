using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("ChannelDestroyed", GatewayIntents.Guilds)]
public abstract class ChannelDestroyedHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected ChannelDestroyedParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (ChannelDestroyedParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.ChannelDestroyed += (SocketChannel) => execution(new ChannelDestroyedParameters(SocketChannel));
}

public record ChannelDestroyedParameters(SocketChannel SocketChannel) : IEventParameters;