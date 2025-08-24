
using System;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Hephaestus.Events;

public abstract partial class WebhooksUpdatedHandler<THandler> : IEventHandler<THandler> where THandler : WebhooksUpdatedHandler<THandler> {

	public abstract Task Execute(SocketGuild arg0, SocketChannel arg1);

	static GatewayIntents[] IEventHandler<THandler>.RequiredIntents { get; } = [ GatewayIntents.None ];

	static void IEventHandler<THandler>.RegisterToClient(DiscordSocketClient client, IServiceProvider services) => client.WebhooksUpdated += services.GetRequiredService<THandler>().Execute;
}

