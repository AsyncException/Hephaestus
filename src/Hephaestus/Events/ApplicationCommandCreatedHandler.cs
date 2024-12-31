using Discord;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("ApplicationCommandCreated", GatewayIntents.None)]
public abstract class ApplicationCommandCreatedHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected ApplicationCommandCreatedParameters Context { get; set; } = default!;

    public abstract Task Execute();

    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (ApplicationCommandCreatedParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {
        client.ApplicationCommandCreated += async (arg1) => await execution(client, services, key, new ApplicationCommandCreatedParameters(arg1));
    }
}

public record ApplicationCommandCreatedParameters(SocketApplicationCommand SocketApplicationCommand) : IEventParameters;