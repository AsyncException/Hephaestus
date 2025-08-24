
using System;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Hephaestus.Events;

public abstract partial class SubscriptionCreatedHandler<THandler> : IEventHandler<THandler> where THandler : SubscriptionCreatedHandler<THandler> {

	public abstract Task Execute(SocketSubscription arg0);

	static GatewayIntents[] IEventHandler<THandler>.RequiredIntents { get; } = [ GatewayIntents.None ];

	static void IEventHandler<THandler>.RegisterToClient(DiscordSocketClient client, IServiceProvider services) => client.SubscriptionCreated += services.GetRequiredService<THandler>().Execute;
}

