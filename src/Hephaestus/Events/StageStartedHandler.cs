using Discord;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("StageStarted", GatewayIntents.None)]
public abstract class StageStartedHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected StageStartedParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (StageStartedParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {

        client.StageStarted += (arg1) => execution(client, services, key, new StageStartedParameters(arg1));
    }

}

public record StageStartedParameters(SocketStageChannel SocketStageChannel) : IEventParameters;