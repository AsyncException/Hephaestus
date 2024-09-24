using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("SpeakerRemoved", GatewayIntents.None)]
public abstract class SpeakerRemovedHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected SpeakerRemovedParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (SpeakerRemovedParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.SpeakerRemoved += (SocketStageChannel, SocketGuildUser) => execution(new SpeakerRemovedParameters(SocketStageChannel, SocketGuildUser));
}

public record SpeakerRemovedParameters(SocketStageChannel SocketStageChannel, SocketGuildUser SocketGuildUser) : IEventParameters;