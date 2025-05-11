
using System;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Hephaestus.Events;

public abstract partial class MessageReceivedHandler<THandler> : IEventHandler<THandler> where THandler : MessageReceivedHandler<THandler> {

	public abstract Task Execute(SocketMessage arg0);

	static GatewayIntents[] IEventHandler<THandler>.RequiredIntents { get; } = [ GatewayIntents.None ];

	static void IEventHandler<THandler>.RegisterToClient(DiscordSocketClient client, IServiceProvider services) => client.MessageReceived += services.GetRequiredService<THandler>().Execute;
}

