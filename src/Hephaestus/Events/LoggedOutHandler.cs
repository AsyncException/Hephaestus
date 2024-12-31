using Discord;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("LoggedOut", GatewayIntents.None)]
public abstract class LoggedOutHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected LoggedOutParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (LoggedOutParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {

        client.LoggedOut += () => execution(client, services, key, new LoggedOutParameters());
    }

}

public record LoggedOutParameters() : IEventParameters;