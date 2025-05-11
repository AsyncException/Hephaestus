
using System;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Hephaestus.Events;

public abstract partial class GuildScheduledEventUpdatedHandler<THandler> : IEventHandler<THandler> where THandler : GuildScheduledEventUpdatedHandler<THandler> {

	public abstract Task Execute(Cacheable<SocketGuildEvent, UInt64> arg0, SocketGuildEvent arg1);

	static GatewayIntents[] IEventHandler<THandler>.RequiredIntents { get; } = [ GatewayIntents.None ];

	static void IEventHandler<THandler>.RegisterToClient(DiscordSocketClient client, IServiceProvider services) => client.GuildScheduledEventUpdated += services.GetRequiredService<THandler>().Execute;
}

