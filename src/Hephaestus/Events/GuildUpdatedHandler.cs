using Discord;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("GuildUpdated", GatewayIntents.Guilds)]
public abstract class GuildUpdatedHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected GuildUpdatedParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (GuildUpdatedParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {

        client.GuildUpdated += (arg1, arg2) => execution(client, services, key, new GuildUpdatedParameters(arg1, arg2));
    }

}

public record GuildUpdatedParameters(SocketGuild OldSocketGuild, SocketGuild SocketGuild) : IEventParameters;