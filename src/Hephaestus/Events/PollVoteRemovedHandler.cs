using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("PollVoteRemoved", GatewayIntents.GuildMessagePolls)]
public abstract class PollVoteRemovedHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected PollVoteRemovedParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (PollVoteRemovedParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {

        client.PollVoteRemoved += (arg1, arg2, arg3, arg4, arg5) => execution(client, services, key, new PollVoteRemovedParameters(arg1, arg2, arg3, arg4, arg5));
    }

}

public record PollVoteRemovedParameters(Cacheable<IUser, ulong> User, Cacheable<ISocketMessageChannel, IRestMessageChannel, IMessageChannel, ulong> SocketMessageChannel, Cacheable<IUserMessage, ulong> UserMessage, Cacheable<SocketGuild, RestGuild, IGuild, ulong>? SocketGuild, ulong Id) : IEventParameters;