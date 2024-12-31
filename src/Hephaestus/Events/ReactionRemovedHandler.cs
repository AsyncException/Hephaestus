using Discord;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("ReactionRemoved", GatewayIntents.GuildMessageReactions)]
public abstract class ReactionRemovedHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected ReactionRemovedParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (ReactionRemovedParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {

        client.ReactionRemoved += (arg1, arg2, arg3) => execution(client, services, key, new ReactionRemovedParameters(arg1, arg2, arg3));
    }

}

public record ReactionRemovedParameters(Cacheable<IUserMessage, ulong> UserMessage, Cacheable<IMessageChannel, ulong> MessageChannel, SocketReaction SocketReaction) : IEventParameters;