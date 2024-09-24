using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("GuildUpdated", GatewayIntents.Guilds)]
public abstract class GuildUpdatedHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected GuildUpdatedParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (GuildUpdatedParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.GuildUpdated += (OldSocketGuild, SocketGuild) => execution(new GuildUpdatedParameters(OldSocketGuild, SocketGuild));
}

public record GuildUpdatedParameters(SocketGuild OldSocketGuild, SocketGuild SocketGuild) : IEventParameters;