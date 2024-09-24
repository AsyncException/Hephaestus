using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("RecipientAdded", GatewayIntents.None)]
public abstract class RecipientAddedHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected RecipientAddedParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (RecipientAddedParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.RecipientAdded += (SocketGroupUser) => execution(new RecipientAddedParameters(SocketGroupUser));
}

public record RecipientAddedParameters(SocketGroupUser SocketGroupUser) : IEventParameters;