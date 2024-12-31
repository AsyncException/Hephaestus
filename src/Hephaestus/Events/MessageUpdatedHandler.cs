using Discord;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("MessageUpdated", GatewayIntents.GuildMessages)]
public abstract class MessageUpdatedHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected MessageUpdatedParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (MessageUpdatedParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {

        client.MessageUpdated += (arg1, arg2, arg3) => execution(client, services, key, new MessageUpdatedParameters(arg1, arg2, arg3));
    }

}

public record MessageUpdatedParameters(Cacheable<IMessage, ulong> OldMessage, SocketMessage Message, ISocketMessageChannel Channel) : IEventParameters;