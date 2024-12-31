using Discord;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("EntitlementCreated", GatewayIntents.None)]
public abstract class EntitlementCreatedHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected EntitlementCreatedParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (EntitlementCreatedParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {

        client.EntitlementCreated += (arg1) => execution(client, services, key, new EntitlementCreatedParameters(arg1));
    }

}

public record EntitlementCreatedParameters(SocketEntitlement SocketEntitlement) : IEventParameters;