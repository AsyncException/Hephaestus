
using System;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Hephaestus.Events;

public abstract partial class MessageCommandExecutedHandler<THandler> : IEventHandler<THandler> where THandler : MessageCommandExecutedHandler<THandler> {

	public abstract Task Execute(SocketMessageCommand arg0);

	static GatewayIntents[] IEventHandler<THandler>.RequiredIntents { get; } = [ GatewayIntents.None ];

	static void IEventHandler<THandler>.RegisterToClient(DiscordSocketClient client, IServiceProvider services) => client.MessageCommandExecuted += services.GetRequiredService<THandler>().Execute;
}

