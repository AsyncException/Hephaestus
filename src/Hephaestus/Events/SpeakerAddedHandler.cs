using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("SpeakerAdded", GatewayIntents.None)]
public abstract class SpeakerAddedHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected SpeakerAddedParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (SpeakerAddedParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.SpeakerAdded += (SocketStageChannel, SocketGuildUser) => execution(new SpeakerAddedParameters(SocketStageChannel, SocketGuildUser));
}

public record SpeakerAddedParameters(SocketStageChannel SocketStageChannel, SocketGuildUser SocketGuildUser) : IEventParameters;