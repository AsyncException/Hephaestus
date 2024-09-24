using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("PresenceUpdated", GatewayIntents.GuildPresences)]
public abstract class PresenceUpdatedHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected PresenceUpdatedParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (PresenceUpdatedParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.PresenceUpdated += (SocketUser, OldSocketPresence, SocketPresence) => execution(new PresenceUpdatedParameters(SocketUser, OldSocketPresence, SocketPresence));
}

public record PresenceUpdatedParameters(SocketUser SocketUser, SocketPresence OldSocketPresence, SocketPresence SocketPresence) : IEventParameters;