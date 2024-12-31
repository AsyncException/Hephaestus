using Discord;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("IntegrationDeleted", GatewayIntents.None)]
public abstract class IntegrationDeletedHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected IntegrationDeletedParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (IntegrationDeletedParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {

        client.IntegrationDeleted += (arg1, arg2, arg3) => execution(client, services, key, new IntegrationDeletedParameters(arg1, arg2, arg3));
    }

}

public record IntegrationDeletedParameters(IGuild Guild, ulong Id, Optional<ulong> IHaveNoClueAtThisPointPleaseUpdateTheDocumentation) : IEventParameters;