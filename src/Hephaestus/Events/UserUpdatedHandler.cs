using Discord;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("UserUpdated", GatewayIntents.Guilds)]
public abstract class UserUpdatedHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected UserUpdatedParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (UserUpdatedParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {

        client.UserUpdated += (arg1, arg2) => execution(client, services, key, new UserUpdatedParameters(arg1, arg2));
    }

}

public record UserUpdatedParameters(SocketUser OldSocketUser, SocketUser SocketUser) : IEventParameters;