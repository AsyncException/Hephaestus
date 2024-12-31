using Discord;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("ModalSubmitted", GatewayIntents.None)]
public abstract class ModalSubmittedHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected ModalSubmittedParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (ModalSubmittedParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {

        client.ModalSubmitted += (arg1) => execution(client, services, key, new ModalSubmittedParameters(arg1));
    }

}

public record ModalSubmittedParameters(SocketModal SocketModal) : IEventParameters;