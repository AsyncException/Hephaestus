using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("EntitlementCreated", GatewayIntents.None)]
public abstract class EntitlementCreatedHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected EntitlementCreatedParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (EntitlementCreatedParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.EntitlementCreated += (SocketEntitlement) => execution(new EntitlementCreatedParameters(SocketEntitlement));
}

public record EntitlementCreatedParameters(SocketEntitlement SocketEntitlement) : IEventParameters;