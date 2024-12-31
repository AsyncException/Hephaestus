using Discord;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("ReactionAdded", GatewayIntents.GuildMessageReactions)]
public abstract class ReactionAddedHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected ReactionAddedParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (ReactionAddedParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {

        client.ReactionAdded += (arg1, arg2, arg3) => execution(client, services, key, new ReactionAddedParameters(arg1, arg2, arg3));
    }

}

public record ReactionAddedParameters(Cacheable<IUserMessage, ulong> UserMessage, Cacheable<IMessageChannel, ulong> MessageChannel, SocketReaction SocketReaction) : IEventParameters;