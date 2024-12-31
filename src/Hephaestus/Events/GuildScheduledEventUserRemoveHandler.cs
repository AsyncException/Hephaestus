using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("GuildScheduledEventUserRemove", GatewayIntents.GuildScheduledEvents)]
public abstract class GuildScheduledEventUserRemoveHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected GuildScheduledEventUserRemoveParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (GuildScheduledEventUserRemoveParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {

        client.GuildScheduledEventUserRemove += (arg1, arg2) => execution(client, services, key, new GuildScheduledEventUserRemoveParameters(arg1, arg2));
    }

}

public record GuildScheduledEventUserRemoveParameters(Cacheable<SocketUser, RestUser, IUser, ulong> User, SocketGuildEvent SocketGuildEvent) : IEventParameters;