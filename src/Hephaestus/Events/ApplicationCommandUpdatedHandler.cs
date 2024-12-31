using Discord;using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("ApplicationCommandUpdated", GatewayIntents.None)]
public abstract class ApplicationCommandUpdatedHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected ApplicationCommandUpdatedParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (ApplicationCommandUpdatedParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {
        client.ApplicationCommandUpdated += (arg1) => execution(client, services, key, new ApplicationCommandUpdatedParameters(arg1));
    }
}

public record ApplicationCommandUpdatedParameters(SocketApplicationCommand SocketApplicationCommand) : IEventParameters;