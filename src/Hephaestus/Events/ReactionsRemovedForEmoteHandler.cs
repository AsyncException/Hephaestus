using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("ReactionsRemovedForEmote", GatewayIntents.GuildMessageReactions)]
public abstract class ReactionsRemovedForEmoteHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected ReactionsRemovedForEmoteParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (ReactionsRemovedForEmoteParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.ReactionsRemovedForEmote += (UserMessage, MessageChannel, Emote) => execution(new ReactionsRemovedForEmoteParameters(UserMessage, MessageChannel, Emote));
}

public record ReactionsRemovedForEmoteParameters(Cacheable<IUserMessage, ulong> UserMessage, Cacheable<IMessageChannel, ulong> MessageChannel, IEmote Emote) : IEventParameters;