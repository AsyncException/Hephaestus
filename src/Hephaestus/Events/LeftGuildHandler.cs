
using System;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Hephaestus.Events;

public abstract partial class LeftGuildHandler<THandler> : IEventHandler<THandler> where THandler : LeftGuildHandler<THandler> {

	public abstract Task Execute(SocketGuild arg0);

	static GatewayIntents[] IEventHandler<THandler>.RequiredIntents { get; } = [ GatewayIntents.None ];

	static void IEventHandler<THandler>.RegisterToClient(DiscordSocketClient client, IServiceProvider services) => client.LeftGuild += services.GetRequiredService<THandler>().Execute;
}

