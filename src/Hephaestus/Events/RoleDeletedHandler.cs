
using System;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Hephaestus.Events;

public abstract partial class RoleDeletedHandler<THandler> : IEventHandler<THandler> where THandler : RoleDeletedHandler<THandler> {

	public abstract Task Execute(SocketRole arg0);

	static GatewayIntents[] IEventHandler<THandler>.RequiredIntents { get; } = [ GatewayIntents.None ];

	static void IEventHandler<THandler>.RegisterToClient(DiscordSocketClient client, IServiceProvider services) => client.RoleDeleted += services.GetRequiredService<THandler>().Execute;
}

