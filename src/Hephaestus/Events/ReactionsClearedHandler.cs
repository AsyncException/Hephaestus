using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("ReactionsCleared", GatewayIntents.GuildMessageReactions)]
public abstract class ReactionsClearedHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected ReactionsClearedParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (ReactionsClearedParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.ReactionsCleared += (UserMessage, MessageChannel) => execution(new ReactionsClearedParameters(UserMessage, MessageChannel));
}

public record ReactionsClearedParameters(Cacheable<IUserMessage, ulong> UserMessage, Cacheable<IMessageChannel, ulong> MessageChannel) : IEventParameters;