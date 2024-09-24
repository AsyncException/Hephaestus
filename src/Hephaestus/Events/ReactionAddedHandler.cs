using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("ReactionAdded", GatewayIntents.GuildMessageReactions)]
public abstract class ReactionAddedHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected ReactionAddedParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (ReactionAddedParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.ReactionAdded += (UserMessage, MessageChannel, SocketReaction) => execution(new ReactionAddedParameters(UserMessage, MessageChannel, SocketReaction));
}

public record ReactionAddedParameters(Cacheable<IUserMessage, ulong> UserMessage, Cacheable<IMessageChannel, ulong> MessageChannel, SocketReaction SocketReaction) : IEventParameters;