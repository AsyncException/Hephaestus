
using System;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Hephaestus.Events;

public abstract partial class SelectMenuExecutedHandler<THandler> : IEventHandler<THandler> where THandler : SelectMenuExecutedHandler<THandler> {

	public abstract Task Execute(SocketMessageComponent arg0);

	static GatewayIntents[] IEventHandler<THandler>.RequiredIntents { get; } = [ GatewayIntents.None ];

	static void IEventHandler<THandler>.RegisterToClient(DiscordSocketClient client, IServiceProvider services) => client.SelectMenuExecuted += services.GetRequiredService<THandler>().Execute;
}

