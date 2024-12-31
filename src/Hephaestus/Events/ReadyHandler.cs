using Discord;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("Ready", GatewayIntents.None)]
public abstract class ReadyHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected ReadyParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (ReadyParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {

        client.Ready += () => execution(client, services, key, new ReadyParameters());
    }

}

public record ReadyParameters() : IEventParameters;