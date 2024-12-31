using Discord;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("UserIsTyping", GatewayIntents.GuildMessageTyping)]
public abstract class UserIsTypingHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected UserIsTypingParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (UserIsTypingParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {

        client.UserIsTyping += (arg1, arg2) => execution(client, services, key, new UserIsTypingParameters(arg1, arg2));
    }

}

public record UserIsTypingParameters(Cacheable<IUser, ulong> User, Cacheable<IMessageChannel, ulong> MessageChannel) : IEventParameters;