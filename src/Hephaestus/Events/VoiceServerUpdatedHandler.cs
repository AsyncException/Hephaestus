using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("VoiceServerUpdated", GatewayIntents.None)]
public abstract class VoiceServerUpdatedHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected VoiceServerUpdatedParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (VoiceServerUpdatedParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.VoiceServerUpdated += (SocketVoiceServer) => execution(new VoiceServerUpdatedParameters(SocketVoiceServer));
}

public record VoiceServerUpdatedParameters(SocketVoiceServer SocketVoiceServer) : IEventParameters;