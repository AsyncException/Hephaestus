using Discord;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("RequestToSpeak", GatewayIntents.None)]
public abstract class RequestToSpeakHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected RequestToSpeakParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (RequestToSpeakParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {

        client.RequestToSpeak += (arg1, arg2) => execution(client, services, key, new RequestToSpeakParameters(arg1, arg2));
    }

}

public record RequestToSpeakParameters(SocketStageChannel SocketStageChannel, SocketGuildUser SocketGuildUser) : IEventParameters;