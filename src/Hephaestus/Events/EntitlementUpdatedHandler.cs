using Discord;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("EntitlementUpdated", GatewayIntents.None)]
public abstract class EntitlementUpdatedHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected EntitlementUpdatedParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (EntitlementUpdatedParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {

        client.EntitlementUpdated += (arg1, arg2) => execution(client, services, key, new EntitlementUpdatedParameters(arg1, arg2));
    }

}

public record EntitlementUpdatedParameters(Cacheable<SocketEntitlement, ulong> OldSocketEntitlement, SocketEntitlement SocketEntitlement) : IEventParameters;