using Discord;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("ChannelUpdated", GatewayIntents.Guilds)]
public abstract class ChannelUpdatedHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected ChannelUpdatedParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (ChannelUpdatedParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {

        client.ChannelUpdated += (arg1, arg2) => execution(client, services, key, new ChannelUpdatedParameters(arg1, arg2));
    }
}

public record ChannelUpdatedParameters(SocketChannel OldSocketChannel, SocketChannel SocketChannel) : IEventParameters;