using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("SentRequest", GatewayIntents.None)]
public abstract class SentRequestHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected SentRequestParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (SentRequestParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.SentRequest += (Method, Endpoint, CompletionTime) => execution(new SentRequestParameters(Method, Endpoint, CompletionTime));
}

public record SentRequestParameters(string Method, string Endpoint, double CompletionTime) : IEventParameters;