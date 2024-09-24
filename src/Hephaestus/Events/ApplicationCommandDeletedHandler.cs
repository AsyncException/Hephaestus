using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("ApplicationCommandDeleted", GatewayIntents.None)]
public abstract class ApplicationCommandDeletedHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected ApplicationCommandDeletedParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (ApplicationCommandDeletedParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.ApplicationCommandDeleted += (SocketApplicationCommand) => execution(new ApplicationCommandDeletedParameters(SocketApplicationCommand));
}

public record ApplicationCommandDeletedParameters(SocketApplicationCommand SocketApplicationCommand) : IEventParameters;