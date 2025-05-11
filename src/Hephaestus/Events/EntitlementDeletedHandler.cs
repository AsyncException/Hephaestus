
using System;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Hephaestus.Events;

public abstract partial class EntitlementDeletedHandler<THandler> : IEventHandler<THandler> where THandler : EntitlementDeletedHandler<THandler> {

	public abstract Task Execute(Cacheable<SocketEntitlement, UInt64> arg0);

	static GatewayIntents[] IEventHandler<THandler>.RequiredIntents { get; } = [ GatewayIntents.None ];

	static void IEventHandler<THandler>.RegisterToClient(DiscordSocketClient client, IServiceProvider services) => client.EntitlementDeleted += services.GetRequiredService<THandler>().Execute;
}

