
using System;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Hephaestus.Events;

public abstract partial class UnknownDispatchReceivedHandler<THandler> : IEventHandler<THandler> where THandler : UnknownDispatchReceivedHandler<THandler> {

	public abstract Task Execute(String arg0, JToken arg1);

	static GatewayIntents[] IEventHandler<THandler>.RequiredIntents { get; } = [ GatewayIntents.None ];

	static void IEventHandler<THandler>.RegisterToClient(DiscordSocketClient client, IServiceProvider services) => client.UnknownDispatchReceived += services.GetRequiredService<THandler>().Execute;
}

