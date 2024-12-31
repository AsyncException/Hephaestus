using Discord;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("GuildScheduledEventCreated", GatewayIntents.GuildScheduledEvents)]
public abstract class GuildScheduledEventCreatedHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected GuildScheduledEventCreatedParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (GuildScheduledEventCreatedParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {

        client.GuildScheduledEventCreated += (arg1) => execution(client, services, key, new GuildScheduledEventCreatedParameters(arg1));
    }

}

public record GuildScheduledEventCreatedParameters(SocketGuildEvent SocketGuildEvent) : IEventParameters;