
using System;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Hephaestus.Events;

public abstract partial class GuildScheduledEventCompletedHandler<THandler> : IEventHandler<THandler> where THandler : GuildScheduledEventCompletedHandler<THandler> {

	public abstract Task Execute(SocketGuildEvent arg0);

	static GatewayIntents[] IEventHandler<THandler>.RequiredIntents { get; } = [ GatewayIntents.None ];

	static void IEventHandler<THandler>.RegisterToClient(DiscordSocketClient client, IServiceProvider services) => client.GuildScheduledEventCompleted += services.GetRequiredService<THandler>().Execute;
}

