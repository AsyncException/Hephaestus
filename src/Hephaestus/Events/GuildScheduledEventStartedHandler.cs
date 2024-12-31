using Discord;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("GuildScheduledEventStarted", GatewayIntents.None)]
public abstract class GuildScheduledEventStartedHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected GuildScheduledEventStartedParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (GuildScheduledEventStartedParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {

        client.GuildScheduledEventStarted += (arg1) => execution(client, services, key, new GuildScheduledEventStartedParameters(arg1));
    }

}

public record GuildScheduledEventStartedParameters(SocketGuildEvent SocketGuildEvent) : IEventParameters;