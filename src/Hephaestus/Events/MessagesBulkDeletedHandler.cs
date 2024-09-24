using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("MessagesBulkDeleted", GatewayIntents.GuildMessages)]
public abstract class MessagesBulkDeletedHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected MessagesBulkDeletedParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (MessagesBulkDeletedParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.MessagesBulkDeleted += (Messages, MessageChannel) => execution(new MessagesBulkDeletedParameters(Messages, MessageChannel));
}

public record MessagesBulkDeletedParameters(IReadOnlyCollection<Cacheable<IMessage, ulong>> Messages, Cacheable<IMessageChannel, ulong> MessageChannel) : IEventParameters;