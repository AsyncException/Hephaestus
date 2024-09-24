using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("GuildMembersDownloaded", GatewayIntents.None)]
public abstract class GuildMembersDownloadedHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected GuildMembersDownloadedParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (GuildMembersDownloadedParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.GuildMembersDownloaded += (SocketGuild) => execution(new GuildMembersDownloadedParameters(SocketGuild));
}

public record GuildMembersDownloadedParameters(SocketGuild SocketGuild) : IEventParameters;