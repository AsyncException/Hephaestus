using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("StageEnded", GatewayIntents.None)]
public abstract class StageEndedHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected StageEndedParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (StageEndedParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.StageEnded += (SocketStageChannel) => execution(new StageEndedParameters(SocketStageChannel));
}

public record StageEndedParameters(SocketStageChannel SocketStageChannel) : IEventParameters;