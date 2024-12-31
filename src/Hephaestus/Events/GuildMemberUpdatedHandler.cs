using Discord;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("GuildMemberUpdated", GatewayIntents.None)]
public abstract class GuildMemberUpdatedHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected GuildMemberUpdatedParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (GuildMemberUpdatedParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {

        client.GuildMemberUpdated += (arg1, arg2) => execution(client, services, key, new GuildMemberUpdatedParameters(arg1, arg2));
    }

}

public record GuildMemberUpdatedParameters(Cacheable<SocketGuildUser, ulong> OldSocketGuildUser, SocketGuildUser SocketGuildUser) : IEventParameters;