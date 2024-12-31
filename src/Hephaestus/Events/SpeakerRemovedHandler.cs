using Discord;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("SpeakerRemoved", GatewayIntents.None)]
public abstract class SpeakerRemovedHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected SpeakerRemovedParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (SpeakerRemovedParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {

        client.SpeakerRemoved += (arg1, arg2) => execution(client, services, key, new SpeakerRemovedParameters(arg1, arg2));
    }

}

public record SpeakerRemovedParameters(SocketStageChannel SocketStageChannel, SocketGuildUser SocketGuildUser) : IEventParameters;