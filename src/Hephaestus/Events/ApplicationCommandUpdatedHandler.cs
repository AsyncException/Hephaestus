using Discord.WebSocket;
using Discord;
using EventHandler = Hephaestus.EventHandling.EventHandler;
using Hephaestus.EventHandling;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("ApplicationCommandUpdated", GatewayIntents.None)]
public abstract class ApplicationCommandUpdatedHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected ApplicationCommandUpdatedParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (ApplicationCommandUpdatedParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.ApplicationCommandUpdated += (SocketApplicationCommand) => execution(new ApplicationCommandUpdatedParameters(SocketApplicationCommand));
}

public record ApplicationCommandUpdatedParameters(SocketApplicationCommand SocketApplicationCommand) : IEventParameters;