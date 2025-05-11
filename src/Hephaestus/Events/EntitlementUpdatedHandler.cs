
using System;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Hephaestus.Events;

public abstract partial class EntitlementUpdatedHandler<THandler> : IEventHandler<THandler> where THandler : EntitlementUpdatedHandler<THandler> {

	public abstract Task Execute(Cacheable<SocketEntitlement, UInt64> arg0, SocketEntitlement arg1);

	static GatewayIntents[] IEventHandler<THandler>.RequiredIntents { get; } = [ GatewayIntents.None ];

	static void IEventHandler<THandler>.RegisterToClient(DiscordSocketClient client, IServiceProvider services) => client.EntitlementUpdated += services.GetRequiredService<THandler>().Execute;
}

