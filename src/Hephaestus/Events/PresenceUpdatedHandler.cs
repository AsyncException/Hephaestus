using Discord;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("PresenceUpdated", GatewayIntents.GuildPresences)]
public abstract class PresenceUpdatedHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected PresenceUpdatedParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (PresenceUpdatedParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {

        client.PresenceUpdated += (arg1, arg2, arg3) => execution(client, services, key, new PresenceUpdatedParameters(arg1, arg2, arg3));
    }

}

public record PresenceUpdatedParameters(SocketUser SocketUser, SocketPresence OldSocketPresence, SocketPresence SocketPresence) : IEventParameters;