
using System;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Hephaestus.Events;

public abstract partial class GuildJoinRequestDeletedHandler<THandler> : IEventHandler<THandler> where THandler : GuildJoinRequestDeletedHandler<THandler> {

	public abstract Task Execute(Cacheable<SocketGuildUser, UInt64> arg0, SocketGuild arg1);

	static GatewayIntents[] IEventHandler<THandler>.RequiredIntents { get; } = [ GatewayIntents.None ];

	static void IEventHandler<THandler>.RegisterToClient(DiscordSocketClient client, IServiceProvider services) => client.GuildJoinRequestDeleted += services.GetRequiredService<THandler>().Execute;
}

