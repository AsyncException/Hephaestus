using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("ThreadMemberLeft", GatewayIntents.None)]
public abstract class ThreadMemberLeftHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected ThreadMemberLeftParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (ThreadMemberLeftParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.ThreadMemberLeft += (SocketThreadUser) => execution(new ThreadMemberLeftParameters(SocketThreadUser));
}

public record ThreadMemberLeftParameters(SocketThreadUser SocketThreadUser) : IEventParameters;