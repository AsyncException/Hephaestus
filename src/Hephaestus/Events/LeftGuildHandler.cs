using Discord;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("LeftGuild", GatewayIntents.None)]
public abstract class LeftGuildHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected LeftGuildParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (LeftGuildParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {

        client.LeftGuild += (arg1) => execution(client, services, key, new LeftGuildParameters(arg1));
    }

}

public record LeftGuildParameters(SocketGuild SocketGuild) : IEventParameters;