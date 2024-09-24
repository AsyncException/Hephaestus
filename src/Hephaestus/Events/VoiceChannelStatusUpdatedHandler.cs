using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("VoiceChannelStatusUpdated", GatewayIntents.None)]
public abstract class VoiceChannelStatusUpdatedHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected VoiceChannelStatusUpdatedParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (VoiceChannelStatusUpdatedParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.VoiceChannelStatusUpdated += (SocketVoiceChannel, OldState, State) => execution(new VoiceChannelStatusUpdatedParameters(SocketVoiceChannel, OldState, State));
}

public record VoiceChannelStatusUpdatedParameters(Cacheable<SocketVoiceChannel, ulong> SocketVoiceChannel, string OldState, string State) : IEventParameters;