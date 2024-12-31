using Discord;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("ReactionsRemovedForEmote", GatewayIntents.GuildMessageReactions)]
public abstract class ReactionsRemovedForEmoteHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected ReactionsRemovedForEmoteParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (ReactionsRemovedForEmoteParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {

        client.ReactionsRemovedForEmote += (arg1, arg2, arg3) => execution(client, services, key, new ReactionsRemovedForEmoteParameters(arg1, arg2, arg3));
    }

}

public record ReactionsRemovedForEmoteParameters(Cacheable<IUserMessage, ulong> UserMessage, Cacheable<IMessageChannel, ulong> MessageChannel, IEmote Emote) : IEventParameters;