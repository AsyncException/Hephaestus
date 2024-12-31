using Discord;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("GuildMembersDownloaded", GatewayIntents.None)]
public abstract class GuildMembersDownloadedHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected GuildMembersDownloadedParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (GuildMembersDownloadedParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {

        client.GuildMembersDownloaded += (arg1) => execution(client, services, key, new GuildMembersDownloadedParameters(arg1));
    }

}

public record GuildMembersDownloadedParameters(SocketGuild SocketGuild) : IEventParameters;