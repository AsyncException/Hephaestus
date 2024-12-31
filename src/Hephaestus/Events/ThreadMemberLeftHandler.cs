using Discord;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("ThreadMemberLeft", GatewayIntents.None)]
public abstract class ThreadMemberLeftHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected ThreadMemberLeftParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (ThreadMemberLeftParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {

        client.ThreadMemberLeft += (arg1) => execution(client, services, key, new ThreadMemberLeftParameters(arg1));
    }

}

public record ThreadMemberLeftParameters(SocketThreadUser SocketThreadUser) : IEventParameters;