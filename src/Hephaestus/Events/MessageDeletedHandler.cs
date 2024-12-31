using Discord;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("MessageDeleted", GatewayIntents.GuildMessages)]
public abstract class MessageDeletedHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected MessageDeletedParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (MessageDeletedParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {

        client.MessageDeleted += (arg1, arg2) => execution(client, services, key, new MessageDeletedParameters(arg1, arg2));
    }

}

public record MessageDeletedParameters(Cacheable<IMessage, ulong> Message, Cacheable<IMessageChannel, ulong> MessageChannel) : IEventParameters;