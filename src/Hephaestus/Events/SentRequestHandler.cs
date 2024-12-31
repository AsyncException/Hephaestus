using Discord;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("SentRequest", GatewayIntents.None)]
public abstract class SentRequestHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected SentRequestParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (SentRequestParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {

        client.SentRequest += (arg1, arg2, arg3) => execution(client, services, key, new SentRequestParameters(arg1, arg2, arg3));
    }

}

public record SentRequestParameters(string Method, string Endpoint, double CompletionTime) : IEventParameters;