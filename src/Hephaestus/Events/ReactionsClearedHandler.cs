using Discord;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("ReactionsCleared", GatewayIntents.GuildMessageReactions)]
public abstract class ReactionsClearedHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected ReactionsClearedParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (ReactionsClearedParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {

        client.ReactionsCleared += (arg1, arg2) => execution(client, services, key, new ReactionsClearedParameters(arg1, arg2));
    }

}

public record ReactionsClearedParameters(Cacheable<IUserMessage, ulong> UserMessage, Cacheable<IMessageChannel, ulong> MessageChannel) : IEventParameters;