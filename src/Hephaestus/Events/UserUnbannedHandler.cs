using Discord;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("UserUnbanned", GatewayIntents.GuildBans)]
public abstract class UserUnbannedHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected UserUnbannedParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (UserUnbannedParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {

        client.UserUnbanned += (arg1, arg2) => execution(client, services, key, new UserUnbannedParameters(arg1, arg2));
    }

}

public record UserUnbannedParameters(SocketUser SocketUser, SocketGuild SocketGuild) : IEventParameters;