using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("UserVoiceStateUpdated", GatewayIntents.GuildVoiceStates)]
public abstract class UserVoiceStateUpdatedHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected UserVoiceStateUpdatedParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (UserVoiceStateUpdatedParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.UserVoiceStateUpdated += (SocketUser, OldSocketVoiceState, SocketVoiceState) => execution(new UserVoiceStateUpdatedParameters(SocketUser, OldSocketVoiceState, SocketVoiceState));
}

public record UserVoiceStateUpdatedParameters(SocketUser SocketUser, SocketVoiceState OldSocketVoiceState, SocketVoiceState SocketVoiceState) : IEventParameters;