using Discord;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("GuildUnavailable", GatewayIntents.Guilds)]
public abstract class GuildUnavailableHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected GuildUnavailableParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (GuildUnavailableParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {

        client.GuildUnavailable += (arg1) => execution(client, services, key, new GuildUnavailableParameters(arg1));
    }

}

public record GuildUnavailableParameters(SocketGuild SocketGuild) : IEventParameters;