using Discord;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("ThreadMemberJoined", GatewayIntents.None)]
public abstract class ThreadMemberJoinedHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected ThreadMemberJoinedParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (ThreadMemberJoinedParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {

        client.ThreadMemberJoined += (arg1) => execution(client, services, key, new ThreadMemberJoinedParameters(arg1));
    }

}

public record ThreadMemberJoinedParameters(SocketThreadUser SocketThreadUser) : IEventParameters;