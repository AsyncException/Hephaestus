using Discord;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;


namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("ChannelCreated", GatewayIntents.Guilds)]
public abstract class ChannelCreatedHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected ChannelCreatedParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (ChannelCreatedParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {
        client.ChannelCreated += (arg1) => execution(client, services, key, new ChannelCreatedParameters(arg1));
    }
}

public record ChannelCreatedParameters(SocketChannel SocketChannel) : IEventParameters;