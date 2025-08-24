
using System;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Hephaestus.Events;

public abstract partial class ChannelDestroyedHandler<THandler> : IEventHandler<THandler> where THandler : ChannelDestroyedHandler<THandler> {

	public abstract Task Execute(SocketChannel arg0);

	static GatewayIntents[] IEventHandler<THandler>.RequiredIntents { get; } = [ GatewayIntents.None ];

	static void IEventHandler<THandler>.RegisterToClient(DiscordSocketClient client, IServiceProvider services) => client.ChannelDestroyed += services.GetRequiredService<THandler>().Execute;
}

