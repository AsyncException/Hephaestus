using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("MessageDeleted", GatewayIntents.GuildMessages)]
public abstract class MessageDeletedHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected MessageDeletedParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (MessageDeletedParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.MessageDeleted += (Message, MessageChannel) => execution(new MessageDeletedParameters(Message, MessageChannel));
}

public record MessageDeletedParameters(Cacheable<IMessage, ulong> Message, Cacheable<IMessageChannel, ulong> MessageChannel) : IEventParameters;