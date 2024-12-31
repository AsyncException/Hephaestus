using Discord;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("VoiceServerUpdated", GatewayIntents.None)]
public abstract class VoiceServerUpdatedHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected VoiceServerUpdatedParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (VoiceServerUpdatedParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {

        client.VoiceServerUpdated += (arg1) => execution(client, services, key, new VoiceServerUpdatedParameters(arg1));
    }

}

public record VoiceServerUpdatedParameters(SocketVoiceServer SocketVoiceServer) : IEventParameters;