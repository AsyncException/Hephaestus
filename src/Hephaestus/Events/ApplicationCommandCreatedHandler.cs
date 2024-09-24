using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("ApplicationCommandCreated", GatewayIntents.None)]
public abstract class ApplicationCommandCreatedHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected ApplicationCommandCreatedParameters Context { get; set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (ApplicationCommandCreatedParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.ApplicationCommandCreated += (SocketApplicationCommand) => execution(new ApplicationCommandCreatedParameters(SocketApplicationCommand));
}

public record ApplicationCommandCreatedParameters(SocketApplicationCommand SocketApplicationCommand) : IEventParameters;