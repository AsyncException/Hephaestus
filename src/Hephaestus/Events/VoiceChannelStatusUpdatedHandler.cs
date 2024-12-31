using Discord;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("VoiceChannelStatusUpdated", GatewayIntents.None)]
public abstract class VoiceChannelStatusUpdatedHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected VoiceChannelStatusUpdatedParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (VoiceChannelStatusUpdatedParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {

        client.VoiceChannelStatusUpdated += (arg1, arg2, arg3) => execution(client, services, key, new VoiceChannelStatusUpdatedParameters(arg1, arg2, arg3));
    }

}

public record VoiceChannelStatusUpdatedParameters(Cacheable<SocketVoiceChannel, ulong> SocketVoiceChannel, string OldState, string State) : IEventParameters;