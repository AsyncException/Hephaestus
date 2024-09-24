using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("UserIsTyping", GatewayIntents.GuildMessageTyping)]
public abstract class UserIsTypingHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected UserIsTypingParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (UserIsTypingParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.UserIsTyping += (User, MessageChannel) => execution(new UserIsTypingParameters(User, MessageChannel));
}

public record UserIsTypingParameters(Cacheable<IUser, ulong> User, Cacheable<IMessageChannel, ulong> MessageChannel) : IEventParameters;