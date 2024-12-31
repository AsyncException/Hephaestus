using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("GuildScheduledEventUserAdd", GatewayIntents.GuildScheduledEvents)]
public abstract class GuildScheduledEventUserAddHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected GuildScheduledEventUserAddParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (GuildScheduledEventUserAddParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {

        client.GuildScheduledEventUserAdd += (arg1, arg2) => execution(client, services, key, new GuildScheduledEventUserAddParameters(arg1, arg2));
    }

}

public record GuildScheduledEventUserAddParameters(Cacheable<SocketUser, RestUser, IUser, ulong> User, SocketGuildEvent SocketGuildEvent) : IEventParameters;