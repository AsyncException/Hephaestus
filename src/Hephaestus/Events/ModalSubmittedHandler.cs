using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("ModalSubmitted", GatewayIntents.None)]
public abstract class ModalSubmittedHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected ModalSubmittedParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (ModalSubmittedParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.ModalSubmitted += (SocketModal) => execution(new ModalSubmittedParameters(SocketModal));
}

public record ModalSubmittedParameters(SocketModal SocketModal) : IEventParameters;