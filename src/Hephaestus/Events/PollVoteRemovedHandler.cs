using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("PollVoteRemoved", GatewayIntents.GuildMessagePolls)]
public abstract class PollVoteRemovedHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected PollVoteRemovedParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (PollVoteRemovedParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.PollVoteRemoved += (User, SocketMessageChannel, UserMessage, SocketGuild, Id) => execution(new PollVoteAddedParameters(User, SocketMessageChannel, UserMessage, SocketGuild, Id));
}

public record PollVoteRemovedParameters(Cacheable<IUser, ulong> User, Cacheable<ISocketMessageChannel, IRestMessageChannel, IMessageChannel, ulong> SocketMessageChannel, Cacheable<IUserMessage, ulong> UserMessage, Cacheable<SocketGuild, RestGuild, IGuild, ulong>? SocketGuild, ulong Id) : IEventParameters;