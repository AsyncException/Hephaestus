using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("ChannelCreated", GatewayIntents.Guilds)]
public abstract class ChannelCreatedHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected ChannelCreatedParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (ChannelCreatedParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.ChannelCreated += (SocketChannel) => execution(new ChannelCreatedParameters(SocketChannel));
}

public record ChannelCreatedParameters(SocketChannel SocketChannel) : IEventParameters;