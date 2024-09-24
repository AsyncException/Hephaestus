using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("IntegrationDeleted", GatewayIntents.None)]
public abstract class IntegrationDeletedHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected IntegrationDeletedParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (IntegrationDeletedParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.IntegrationDeleted += (Guild, Id, IHaveNoClueAtThisPointPleaseUpdateTheDocumentation) => execution(new IntegrationDeletedParameters(Guild, Id, IHaveNoClueAtThisPointPleaseUpdateTheDocumentation));
}

public record IntegrationDeletedParameters(IGuild Guild, ulong Id, Optional<ulong> IHaveNoClueAtThisPointPleaseUpdateTheDocumentation) : IEventParameters;