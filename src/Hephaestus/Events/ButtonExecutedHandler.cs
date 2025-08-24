
using System;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Hephaestus.Events;

public abstract partial class ButtonExecutedHandler<THandler> : IEventHandler<THandler> where THandler : ButtonExecutedHandler<THandler> {

	public abstract Task Execute(SocketMessageComponent arg0);

	static GatewayIntents[] IEventHandler<THandler>.RequiredIntents { get; } = [ GatewayIntents.None ];

	static void IEventHandler<THandler>.RegisterToClient(DiscordSocketClient client, IServiceProvider services) => client.ButtonExecuted += services.GetRequiredService<THandler>().Execute;
}

