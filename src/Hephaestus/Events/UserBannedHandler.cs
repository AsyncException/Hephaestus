using Discord;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("UserBanned", GatewayIntents.GuildBans)]
public abstract class UserBannedHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected UserBannedParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (UserBannedParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {

        client.UserBanned += (arg1, arg2) => execution(client, services, key, new UserBannedParameters(arg1, arg2));
    }

}

public record UserBannedParameters(SocketUser SocketUser, SocketGuild SocketGuild) : IEventParameters;