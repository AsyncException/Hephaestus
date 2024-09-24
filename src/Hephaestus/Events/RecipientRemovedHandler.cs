using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("RecipientRemoved", GatewayIntents.None)]
public abstract class RecipientRemovedHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected RecipientRemovedParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (RecipientRemovedParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.RecipientRemoved += (SocketGroupUser) => execution(new RecipientRemovedParameters(SocketGroupUser));
}

public record RecipientRemovedParameters(SocketGroupUser SocketGroupUser) : IEventParameters;