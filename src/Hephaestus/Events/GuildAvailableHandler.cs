using Discord;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("GuildAvailable", GatewayIntents.Guilds)]
public abstract class GuildAvailableHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected GuildAvailableParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (GuildAvailableParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {

        client.GuildAvailable += (arg1) => execution(client, services, key, new GuildAvailableParameters(arg1));
    }

}

public record GuildAvailableParameters(SocketGuild SocketGuild) : IEventParameters;