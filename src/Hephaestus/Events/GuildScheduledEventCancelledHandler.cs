using Discord;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("GuildScheduledEventCancelled", GatewayIntents.GuildScheduledEvents)]
public abstract class GuildScheduledEventCancelledHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected GuildScheduledEventCancelledParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (GuildScheduledEventCancelledParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {

        client.GuildScheduledEventCancelled += (arg1) => execution(client, services, key, new GuildScheduledEventCancelledParameters(arg1));
    }

}

public record GuildScheduledEventCancelledParameters(SocketGuildEvent SocketGuildEvent) : IEventParameters;