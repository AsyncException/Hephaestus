using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("RoleUpdated", GatewayIntents.Guilds)]
public abstract class RoleUpdatedHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected RoleUpdatedParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (RoleUpdatedParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.RoleUpdated += (OldSocketRole, SocketRole) => execution(new RoleUpdatedParameters(OldSocketRole, SocketRole));
}

public record RoleUpdatedParameters(SocketRole OldSocketRole, SocketRole SocketRole) : IEventParameters;