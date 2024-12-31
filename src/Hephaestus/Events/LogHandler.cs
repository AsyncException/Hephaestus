using Discord;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("Log", GatewayIntents.None)]
public abstract class LogHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected LogParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (LogParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {

        client.Log += (arg1) => execution(client, services, key, new LogParameters(arg1));
    }

}

public record LogParameters(LogMessage LogMessage) : IEventParameters;