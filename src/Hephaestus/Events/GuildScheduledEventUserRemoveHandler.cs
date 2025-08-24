
using System;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Hephaestus.Events;

public abstract partial class GuildScheduledEventUserRemoveHandler<THandler> : IEventHandler<THandler> where THandler : GuildScheduledEventUserRemoveHandler<THandler> {

	public abstract Task Execute(Cacheable<SocketUser, RestUser, IUser, UInt64> arg0, SocketGuildEvent arg1);

	static GatewayIntents[] IEventHandler<THandler>.RequiredIntents { get; } = [ GatewayIntents.None ];

	static void IEventHandler<THandler>.RegisterToClient(DiscordSocketClient client, IServiceProvider services) => client.GuildScheduledEventUserRemove += services.GetRequiredService<THandler>().Execute;
}

