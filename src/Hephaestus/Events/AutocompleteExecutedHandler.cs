using Discord;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("AutocompleteExecuted", GatewayIntents.None)]
public abstract class AutocompleteExecutedHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected AutocompleteExecutedParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (AutocompleteExecutedParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {
        client.AutocompleteExecuted += (arg1) => execution(client, services, key, new AutocompleteExecutedParameters(arg1));
    }
}

public record AutocompleteExecutedParameters(SocketAutocompleteInteraction SocketAutocompleteInteraction) : IEventParameters;