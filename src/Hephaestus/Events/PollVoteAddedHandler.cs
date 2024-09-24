using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("PollVoteAdded", GatewayIntents.GuildMessagePolls)]
public abstract class PollVoteAddedHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected PollVoteAddedParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (PollVoteAddedParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.PollVoteAdded += (User, SocketMessageChannel, UserMessage, SocketGuild, Id) => execution(new PollVoteAddedParameters(User, SocketMessageChannel, UserMessage, SocketGuild, Id));
}

public record PollVoteAddedParameters(Cacheable<IUser, ulong> User, Cacheable<ISocketMessageChannel, IRestMessageChannel, IMessageChannel, ulong> SocketMessageChannel, Cacheable<IUserMessage, ulong> UserMessage, Cacheable<SocketGuild, RestGuild, IGuild, ulong>? SocketGuild, ulong Id) : IEventParameters;