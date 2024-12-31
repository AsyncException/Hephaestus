using Discord;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("MessagesBulkDeleted", GatewayIntents.GuildMessages)]
public abstract class MessagesBulkDeletedHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected MessagesBulkDeletedParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (MessagesBulkDeletedParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {

        client.MessagesBulkDeleted += (arg1, arg2) => execution(client, services, key, new MessagesBulkDeletedParameters(arg1, arg2));
    }

}

public record MessagesBulkDeletedParameters(IReadOnlyCollection<Cacheable<IMessage, ulong>> Messages, Cacheable<IMessageChannel, ulong> MessageChannel) : IEventParameters;