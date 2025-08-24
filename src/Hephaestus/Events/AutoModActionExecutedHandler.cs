
using System;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Hephaestus.Events;

public abstract partial class AutoModActionExecutedHandler<THandler> : IEventHandler<THandler> where THandler : AutoModActionExecutedHandler<THandler> {

	public abstract Task Execute(SocketGuild arg0, AutoModRuleAction arg1, AutoModActionExecutedData arg2);

	static GatewayIntents[] IEventHandler<THandler>.RequiredIntents { get; } = [ GatewayIntents.None ];

	static void IEventHandler<THandler>.RegisterToClient(DiscordSocketClient client, IServiceProvider services) => client.AutoModActionExecuted += services.GetRequiredService<THandler>().Execute;
}

