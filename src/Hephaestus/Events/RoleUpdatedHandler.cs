
using System;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Hephaestus.Events;

public abstract partial class RoleUpdatedHandler<THandler> : IEventHandler<THandler> where THandler : RoleUpdatedHandler<THandler> {

	public abstract Task Execute(SocketRole arg0, SocketRole arg1);

	static GatewayIntents[] IEventHandler<THandler>.RequiredIntents { get; } = [ GatewayIntents.None ];

	static void IEventHandler<THandler>.RegisterToClient(DiscordSocketClient client, IServiceProvider services) => client.RoleUpdated += services.GetRequiredService<THandler>().Execute;
}

