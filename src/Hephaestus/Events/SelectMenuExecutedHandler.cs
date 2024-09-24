using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("SelectMenuExecuted", GatewayIntents.None)]
public abstract class SelectMenuExecutedHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected SelectMenuExecutedParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (SelectMenuExecutedParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.SelectMenuExecuted += (SocketMessageComponent) => execution(new SelectMenuExecutedParameters(SocketMessageComponent));
}

public record SelectMenuExecutedParameters(SocketMessageComponent SocketMessageComponent) : IEventParameters;