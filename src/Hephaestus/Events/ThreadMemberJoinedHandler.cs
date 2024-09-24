using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("ThreadMemberJoined", GatewayIntents.None)]
public abstract class ThreadMemberJoinedHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected ThreadMemberJoinedParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (ThreadMemberJoinedParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.ThreadMemberJoined += (SocketThreadUser) => execution(new ThreadMemberJoinedParameters(SocketThreadUser));
}

public record ThreadMemberJoinedParameters(SocketThreadUser SocketThreadUser) : IEventParameters;