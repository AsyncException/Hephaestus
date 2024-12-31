using Discord;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("StageEnded", GatewayIntents.None)]
public abstract class StageEndedHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected StageEndedParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (StageEndedParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {

        client.StageEnded += (arg1) => execution(client, services, key, new StageEndedParameters(arg1));
    }

}

public record StageEndedParameters(SocketStageChannel SocketStageChannel) : IEventParameters;