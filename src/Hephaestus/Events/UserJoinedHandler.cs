using Discord;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("UserJoined", GatewayIntents.GuildMembers)]
public abstract class UserJoinedHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected UserJoinedParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (UserJoinedParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {

        client.UserJoined += (arg1) => execution(client, services, key, new UserJoinedParameters(arg1));
    }

}

public record UserJoinedParameters(SocketUser SocketUser) : IEventParameters;