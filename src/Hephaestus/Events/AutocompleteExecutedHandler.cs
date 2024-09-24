using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;
namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("AutocompleteExecuted", GatewayIntents.None)]
public abstract class AutocompleteExecutedHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected AutocompleteExecutedParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (AutocompleteExecutedParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.AutocompleteExecuted += (SocketAutocompleteInteraction) => execution(new AutocompleteExecutedParameters(SocketAutocompleteInteraction));
}

public record AutocompleteExecutedParameters(SocketAutocompleteInteraction SocketAutocompleteInteraction) : IEventParameters;