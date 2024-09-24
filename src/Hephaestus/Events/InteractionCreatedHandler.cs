using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("InteractionCreated", GatewayIntents.None)]
public abstract class InteractionCreatedHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected InteractionCreatedParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (InteractionCreatedParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.InteractionCreated += (SocketInteraction) => execution(new InteractionCreatedParameters(SocketInteraction));
}

public record InteractionCreatedParameters(SocketInteraction SocketInteraction) : IEventParameters;