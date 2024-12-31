using Discord;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("Connected", GatewayIntents.None)]
public abstract class ConnectedHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected ConnectedParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (ConnectedParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {
        client.Connected += () => execution(client, services, key, new ConnectedParameters());
    }

}

public record ConnectedParameters() : IEventParameters;