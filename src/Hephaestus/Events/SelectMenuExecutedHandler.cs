using Discord;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("SelectMenuExecuted", GatewayIntents.None)]
public abstract class SelectMenuExecutedHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected SelectMenuExecutedParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (SelectMenuExecutedParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {

        client.SelectMenuExecuted += (arg1) => execution(client, services, key, new SelectMenuExecutedParameters(arg1));
    }

}

public record SelectMenuExecutedParameters(SocketMessageComponent SocketMessageComponent) : IEventParameters;