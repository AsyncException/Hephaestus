using Discord;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("GuildScheduledEventUpdated", GatewayIntents.GuildScheduledEvents)]
public abstract class GuildScheduledEventUpdatedHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected GuildScheduledEventUpdatedParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (GuildScheduledEventUpdatedParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {

        client.GuildScheduledEventUpdated += (arg1, arg2) => execution(client, services, key, new GuildScheduledEventUpdatedParameters(arg1, arg2));
    }

}

public record GuildScheduledEventUpdatedParameters(Cacheable<SocketGuildEvent, ulong> OldSocketGuildEvent, SocketGuildEvent SocketGuildEvent) : IEventParameters;