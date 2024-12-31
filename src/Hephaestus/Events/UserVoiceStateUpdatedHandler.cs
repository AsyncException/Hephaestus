using Discord;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("UserVoiceStateUpdated", GatewayIntents.GuildVoiceStates)]
public abstract class UserVoiceStateUpdatedHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected UserVoiceStateUpdatedParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (UserVoiceStateUpdatedParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {

        client.UserVoiceStateUpdated += (arg1, arg2, arg3) => execution(client, services, key, new UserVoiceStateUpdatedParameters(arg1, arg2, arg3));
    }

}

public record UserVoiceStateUpdatedParameters(SocketUser SocketUser, SocketVoiceState OldSocketVoiceState, SocketVoiceState SocketVoiceState) : IEventParameters;