using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("Disconnected", GatewayIntents.None)]
public abstract class DisconnectedHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected DisconnectedParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (DisconnectedParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.Disconnected += (Exception) => execution(new DisconnectedParameters(Exception));
}

public record DisconnectedParameters(Exception Exception) : IEventParameters;