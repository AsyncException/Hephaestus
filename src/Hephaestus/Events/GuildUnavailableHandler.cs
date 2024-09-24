using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("GuildUnavailable", GatewayIntents.Guilds)]
public abstract class GuildUnavailableHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected GuildUnavailableParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (GuildUnavailableParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.GuildUnavailable += (SocketGuild) => execution(new GuildUnavailableParameters(SocketGuild));
}

public record GuildUnavailableParameters(SocketGuild SocketGuild) : IEventParameters;