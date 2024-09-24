using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("GuildMemberUpdated", GatewayIntents.None)]
public abstract class GuildMemberUpdatedHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected GuildMemberUpdatedParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (GuildMemberUpdatedParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.GuildMemberUpdated += (OldSocketGuildUser, SocketGuildUser) => execution(new GuildMemberUpdatedParameters(OldSocketGuildUser, SocketGuildUser));
}

public record GuildMemberUpdatedParameters(Cacheable<SocketGuildUser, ulong> OldSocketGuildUser, SocketGuildUser SocketGuildUser) : IEventParameters;