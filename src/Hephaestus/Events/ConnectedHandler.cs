using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("Connected", GatewayIntents.None)]
public abstract class ConnectedHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected ConnectedParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (ConnectedParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.Connected += () => execution(new ConnectedParameters());
}

public record ConnectedParameters() : IEventParameters;