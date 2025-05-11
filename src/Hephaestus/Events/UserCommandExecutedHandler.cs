
using System;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Hephaestus.Events;

public abstract partial class UserCommandExecutedHandler<THandler> : IEventHandler<THandler> where THandler : UserCommandExecutedHandler<THandler> {

	public abstract Task Execute(SocketUserCommand arg0);

	static GatewayIntents[] IEventHandler<THandler>.RequiredIntents { get; } = [ GatewayIntents.None ];

	static void IEventHandler<THandler>.RegisterToClient(DiscordSocketClient client, IServiceProvider services) => client.UserCommandExecuted += services.GetRequiredService<THandler>().Execute;
}

