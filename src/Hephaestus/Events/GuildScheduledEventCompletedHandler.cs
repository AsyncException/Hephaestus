using Discord;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("GuildScheduledEventCompleted", GatewayIntents.None)]
public abstract class GuildScheduledEventCompletedHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected GuildScheduledEventCompletedParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (GuildScheduledEventCompletedParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {

        client.GuildScheduledEventCompleted += (arg1) => execution(client, services, key, new GuildScheduledEventCompletedParameters(arg1));
    }

}

public record GuildScheduledEventCompletedParameters(SocketGuildEvent SocketGuildEvent) : IEventParameters;