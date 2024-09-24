using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("EntitlementUpdated", GatewayIntents.None)]
public abstract class EntitlementUpdatedHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected EntitlementUpdatedParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (EntitlementUpdatedParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.EntitlementUpdated += (OldSocketEntitlement, SocketEntitlement) => execution(new EntitlementUpdatedParameters(OldSocketEntitlement, SocketEntitlement));
}

public record EntitlementUpdatedParameters(Cacheable<SocketEntitlement, ulong> OldSocketEntitlement, SocketEntitlement SocketEntitlement) : IEventParameters;