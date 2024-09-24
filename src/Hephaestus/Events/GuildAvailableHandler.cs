using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("GuildAvailable", GatewayIntents.Guilds)]
public abstract class GuildAvailableHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected GuildAvailableParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (GuildAvailableParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.GuildAvailable += (SocketGuild) => execution(new GuildAvailableParameters(SocketGuild));
}

public record GuildAvailableParameters(SocketGuild SocketGuild) : IEventParameters;