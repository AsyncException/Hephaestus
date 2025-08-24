
using System;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Hephaestus.Events;

public abstract partial class PollVoteRemovedHandler<THandler> : IEventHandler<THandler> where THandler : PollVoteRemovedHandler<THandler> {

	public abstract Task Execute(Cacheable<IUser, UInt64> arg0, Cacheable<ISocketMessageChannel, IRestMessageChannel, IMessageChannel, UInt64> arg1, Cacheable<IUserMessage, UInt64> arg2, Nullable<Cacheable<SocketGuild, RestGuild, IGuild, UInt64>> arg3, UInt64 arg4);

	static GatewayIntents[] IEventHandler<THandler>.RequiredIntents { get; } = [ GatewayIntents.None ];

	static void IEventHandler<THandler>.RegisterToClient(DiscordSocketClient client, IServiceProvider services) => client.PollVoteRemoved += services.GetRequiredService<THandler>().Execute;
}

