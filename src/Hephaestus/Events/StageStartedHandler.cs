using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("StageStarted", GatewayIntents.None)]
public abstract class StageStartedHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected StageStartedParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (StageStartedParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.StageStarted += (SocketStageChannel) => execution(new StageStartedParameters(SocketStageChannel));
}

public record StageStartedParameters(SocketStageChannel SocketStageChannel) : IEventParameters;