
using System;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Hephaestus.Events;

public abstract partial class UserBannedHandler<THandler> : IEventHandler<THandler> where THandler : UserBannedHandler<THandler> {

	public abstract Task Execute(SocketUser arg0, SocketGuild arg1);

	static GatewayIntents[] IEventHandler<THandler>.RequiredIntents { get; } = [ GatewayIntents.None ];

	static void IEventHandler<THandler>.RegisterToClient(DiscordSocketClient client, IServiceProvider services) => client.UserBanned += services.GetRequiredService<THandler>().Execute;
}

