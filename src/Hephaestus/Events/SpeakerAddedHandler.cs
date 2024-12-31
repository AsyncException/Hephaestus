using Discord;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("SpeakerAdded", GatewayIntents.None)]
public abstract class SpeakerAddedHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected SpeakerAddedParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (SpeakerAddedParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {

        client.SpeakerAdded += (arg1, arg2) => execution(client, services, key, new SpeakerAddedParameters(arg1, arg2));
    }

}

public record SpeakerAddedParameters(SocketStageChannel SocketStageChannel, SocketGuildUser SocketGuildUser) : IEventParameters;