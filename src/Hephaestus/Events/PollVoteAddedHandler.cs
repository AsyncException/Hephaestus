using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("PollVoteAdded", GatewayIntents.GuildMessagePolls)]
public abstract class PollVoteAddedHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected PollVoteAddedParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (PollVoteAddedParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {

        client.PollVoteAdded += (arg1, arg2, arg3, arg4, arg5) => execution(client, services, key, new PollVoteAddedParameters(arg1, arg2, arg3, arg4, arg5));
    }

}

public record PollVoteAddedParameters(Cacheable<IUser, ulong> User, Cacheable<ISocketMessageChannel, IRestMessageChannel, IMessageChannel, ulong> SocketMessageChannel, Cacheable<IUserMessage, ulong> UserMessage, Cacheable<SocketGuild, RestGuild, IGuild, ulong>? SocketGuild, ulong Id) : IEventParameters;