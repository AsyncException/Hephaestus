
using System;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Hephaestus.Events;

public abstract partial class IntegrationDeletedHandler<THandler> : IEventHandler<THandler> where THandler : IntegrationDeletedHandler<THandler> {

	public abstract Task Execute(IGuild arg0, UInt64 arg1, Optional<UInt64> arg2);

	static GatewayIntents[] IEventHandler<THandler>.RequiredIntents { get; } = [ GatewayIntents.None ];

	static void IEventHandler<THandler>.RegisterToClient(DiscordSocketClient client, IServiceProvider services) => client.IntegrationDeleted += services.GetRequiredService<THandler>().Execute;
}

