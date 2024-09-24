using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("LoggedOut", GatewayIntents.None)]
public abstract class LoggedOutHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected LoggedOutParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (LoggedOutParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.LoggedOut += () => execution(new LoggedOutParameters());
}

public record LoggedOutParameters() : IEventParameters;