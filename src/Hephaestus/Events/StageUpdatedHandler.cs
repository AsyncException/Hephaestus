using Discord;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("StageUpdated", GatewayIntents.None)]
public abstract class StageUpdatedHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected StageUpdatedParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (StageUpdatedParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {

        client.StageUpdated += (arg1, arg2) => execution(client, services, key, new StageUpdatedParameters(arg1, arg2));
    }

}

public record StageUpdatedParameters(SocketStageChannel OldSocketStageChannel, SocketStageChannel SocketStageChannel) : IEventParameters;