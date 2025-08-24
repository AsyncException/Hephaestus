
using System;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Hephaestus.Events;

public abstract partial class AutoModRuleDeletedHandler<THandler> : IEventHandler<THandler> where THandler : AutoModRuleDeletedHandler<THandler> {

	public abstract Task Execute(SocketAutoModRule arg0);

	static GatewayIntents[] IEventHandler<THandler>.RequiredIntents { get; } = [ GatewayIntents.None ];

	static void IEventHandler<THandler>.RegisterToClient(DiscordSocketClient client, IServiceProvider services) => client.AutoModRuleDeleted += services.GetRequiredService<THandler>().Execute;
}

