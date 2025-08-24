
using System;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Hephaestus.Events;

public abstract partial class AuditLogCreatedHandler<THandler> : IEventHandler<THandler> where THandler : AuditLogCreatedHandler<THandler> {

	public abstract Task Execute(SocketAuditLogEntry arg0, SocketGuild arg1);

	static GatewayIntents[] IEventHandler<THandler>.RequiredIntents { get; } = [ GatewayIntents.None ];

	static void IEventHandler<THandler>.RegisterToClient(DiscordSocketClient client, IServiceProvider services) => client.AuditLogCreated += services.GetRequiredService<THandler>().Execute;
}

