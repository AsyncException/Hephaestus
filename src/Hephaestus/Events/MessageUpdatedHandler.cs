using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("MessageUpdated", GatewayIntents.GuildMessages)]
public abstract class MessageUpdatedHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected MessageUpdatedParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (MessageUpdatedParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.MessageUpdated += (OldMessage, Message, Channel) => execution(new MessageUpdatedParameters(OldMessage, Message, Channel));
}

public record MessageUpdatedParameters(Cacheable<IMessage, ulong> OldMessage, SocketMessage Message, ISocketMessageChannel Channel) : IEventParameters;