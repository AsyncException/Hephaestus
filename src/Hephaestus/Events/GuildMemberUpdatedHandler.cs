
using System;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Hephaestus.Events;

public abstract partial class GuildMemberUpdatedHandler<THandler> : IEventHandler<THandler> where THandler : GuildMemberUpdatedHandler<THandler> {

	public abstract Task Execute(Cacheable<SocketGuildUser, UInt64> arg0, SocketGuildUser arg1);

	static GatewayIntents[] IEventHandler<THandler>.RequiredIntents { get; } = [ GatewayIntents.None ];

	static void IEventHandler<THandler>.RegisterToClient(DiscordSocketClient client, IServiceProvider services) => client.GuildMemberUpdated += services.GetRequiredService<THandler>().Execute;
}

