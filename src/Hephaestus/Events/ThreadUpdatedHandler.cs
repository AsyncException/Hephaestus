
using System;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Hephaestus.Events;

public abstract partial class ThreadUpdatedHandler<THandler> : IEventHandler<THandler> where THandler : ThreadUpdatedHandler<THandler> {

	public abstract Task Execute(Cacheable<SocketThreadChannel, UInt64> arg0, SocketThreadChannel arg1);

	static GatewayIntents[] IEventHandler<THandler>.RequiredIntents { get; } = [ GatewayIntents.None ];

	static void IEventHandler<THandler>.RegisterToClient(DiscordSocketClient client, IServiceProvider services) => client.ThreadUpdated += services.GetRequiredService<THandler>().Execute;
}

