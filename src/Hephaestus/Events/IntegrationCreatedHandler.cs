using Discord;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("IntegrationCreated", GatewayIntents.None)]
public abstract class IntegrationCreatedHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected IntegrationCreatedParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (IntegrationCreatedParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {

        client.IntegrationCreated += (arg1) => execution(client, services, key, new IntegrationCreatedParameters(arg1));
    }

}

public record IntegrationCreatedParameters(IIntegration Integration) : IEventParameters;