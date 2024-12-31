using Discord;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("LoggedIn", GatewayIntents.None)]
public abstract class LoggedInHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected LoggedInParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (LoggedInParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {

        client.LoggedIn += () => execution(client, services, key, new LoggedInParameters());
    }

}

public record LoggedInParameters() : IEventParameters;