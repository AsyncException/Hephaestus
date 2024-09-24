using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("ReactionRemoved", GatewayIntents.GuildMessageReactions)]
public abstract class ReactionRemovedHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected ReactionRemovedParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (ReactionRemovedParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.ReactionRemoved += (UserMessage, MessageChannel, SocketReaction) => execution(new ReactionRemovedParameters(UserMessage, MessageChannel, SocketReaction));
}

public record ReactionRemovedParameters(Cacheable<IUserMessage, ulong> UserMessage, Cacheable<IMessageChannel, ulong> MessageChannel, SocketReaction SocketReaction) : IEventParameters;