using Discord;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("UserLeft", GatewayIntents.Guilds)]
public abstract class UserLeftHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected UserLeftParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (UserLeftParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {

        client.UserLeft += (arg1, arg2) => execution(client, services, key, new UserLeftParameters(arg1, arg2));
    }

}

public record UserLeftParameters(SocketGuild SocketGuild, SocketUser SocketUser) : IEventParameters;