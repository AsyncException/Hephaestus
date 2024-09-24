using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("StageUpdated", GatewayIntents.None)]
public abstract class StageUpdatedHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected StageUpdatedParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (StageUpdatedParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.StageUpdated += (OldSocketStageChannel, SocketStageChannel) => execution(new StageUpdatedParameters(OldSocketStageChannel, SocketStageChannel));
}

public record StageUpdatedParameters(SocketStageChannel OldSocketStageChannel, SocketStageChannel SocketStageChannel) : IEventParameters;