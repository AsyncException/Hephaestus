using Discord;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("RecipientRemoved", GatewayIntents.None)]
public abstract class RecipientRemovedHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected RecipientRemovedParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (RecipientRemovedParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {

        client.RecipientRemoved += (arg1) => execution(client, services, key, new RecipientRemovedParameters(arg1));
    }

}

public record RecipientRemovedParameters(SocketGroupUser SocketGroupUser) : IEventParameters;