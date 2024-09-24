using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("RequestToSpeak", GatewayIntents.None)]
public abstract class RequestToSpeakHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected RequestToSpeakParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (RequestToSpeakParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.RequestToSpeak += (SocketStageChannel, SocketGuildUser) => execution(new RequestToSpeakParameters(SocketStageChannel, SocketGuildUser));
}

public record RequestToSpeakParameters(SocketStageChannel SocketStageChannel, SocketGuildUser SocketGuildUser) : IEventParameters;