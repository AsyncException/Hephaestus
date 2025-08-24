
using System;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Hephaestus.Events;

public abstract partial class RoleCreatedHandler<THandler> : IEventHandler<THandler> where THandler : RoleCreatedHandler<THandler> {

	public abstract Task Execute(SocketRole arg0);

	static GatewayIntents[] IEventHandler<THandler>.RequiredIntents { get; } = [ GatewayIntents.None ];

	static void IEventHandler<THandler>.RegisterToClient(DiscordSocketClient client, IServiceProvider services) => client.RoleCreated += services.GetRequiredService<THandler>().Execute;
}

