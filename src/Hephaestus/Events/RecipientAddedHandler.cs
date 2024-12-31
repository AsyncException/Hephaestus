using Discord;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("RecipientAdded", GatewayIntents.None)]
public abstract class RecipientAddedHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected RecipientAddedParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (RecipientAddedParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {

        client.RecipientAdded += (arg1) => execution(client, services, key, new RecipientAddedParameters(arg1));
    }

}

public record RecipientAddedParameters(SocketGroupUser SocketGroupUser) : IEventParameters;