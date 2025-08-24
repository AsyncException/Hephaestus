
using System;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Hephaestus.Events;

public abstract partial class AutoModRuleUpdatedHandler<THandler> : IEventHandler<THandler> where THandler : AutoModRuleUpdatedHandler<THandler> {

	public abstract Task Execute(Cacheable<SocketAutoModRule, UInt64> arg0, SocketAutoModRule arg1);

	static GatewayIntents[] IEventHandler<THandler>.RequiredIntents { get; } = [ GatewayIntents.None ];

	static void IEventHandler<THandler>.RegisterToClient(DiscordSocketClient client, IServiceProvider services) => client.AutoModRuleUpdated += services.GetRequiredService<THandler>().Execute;
}

