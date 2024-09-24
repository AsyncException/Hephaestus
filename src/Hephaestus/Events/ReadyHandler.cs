using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("Ready", GatewayIntents.None)]
public abstract class ReadyHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected ReadyParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (ReadyParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.Ready += () => execution(new ReadyParameters());
}

public record ReadyParameters() : IEventParameters;