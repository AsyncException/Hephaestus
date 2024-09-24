using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("LoggedIn", GatewayIntents.None)]
public abstract class LoggedInHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected LoggedInParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (LoggedInParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.LoggedIn += () => execution(new LoggedInParameters());
}

public record LoggedInParameters() : IEventParameters;