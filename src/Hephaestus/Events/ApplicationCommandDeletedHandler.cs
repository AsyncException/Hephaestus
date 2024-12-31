using Discord;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("ApplicationCommandDeleted", GatewayIntents.None)]
public abstract class ApplicationCommandDeletedHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected ApplicationCommandDeletedParameters Context { get; private set; } = default!;

    public abstract Task Execute();

    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (ApplicationCommandDeletedParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {
        client.ApplicationCommandDeleted += (arg1) => execution(client, services, key, new ApplicationCommandDeletedParameters(arg1));
    }
}

public record ApplicationCommandDeletedParameters(SocketApplicationCommand SocketApplicationCommand) : IEventParameters;