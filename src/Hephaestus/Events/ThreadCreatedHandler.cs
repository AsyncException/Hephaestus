
using System;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Hephaestus.Events;

public abstract partial class ThreadCreatedHandler<THandler> : IEventHandler<THandler> where THandler : ThreadCreatedHandler<THandler> {

	public abstract Task Execute(SocketThreadChannel arg0);

	static GatewayIntents[] IEventHandler<THandler>.RequiredIntents { get; } = [ GatewayIntents.None ];

	static void IEventHandler<THandler>.RegisterToClient(DiscordSocketClient client, IServiceProvider services) => client.ThreadCreated += services.GetRequiredService<THandler>().Execute;
}

