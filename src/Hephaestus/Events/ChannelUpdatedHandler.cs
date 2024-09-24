using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("ChannelUpdated", GatewayIntents.Guilds)]
public abstract class ChannelUpdatedHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected ChannelUpdatedParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (ChannelUpdatedParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.ChannelUpdated += (OldSocketChannel, SocketChannel) => execution(new ChannelUpdatedParameters(OldSocketChannel, SocketChannel));
}

public record ChannelUpdatedParameters(SocketChannel OldSocketChannel, SocketChannel SocketChannel) : IEventParameters;