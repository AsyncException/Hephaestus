
using System;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Hephaestus.Events;

public abstract partial class InviteCreatedHandler<THandler> : IEventHandler<THandler> where THandler : InviteCreatedHandler<THandler> {

	public abstract Task Execute(SocketInvite arg0);

	static GatewayIntents[] IEventHandler<THandler>.RequiredIntents { get; } = [ GatewayIntents.None ];

	static void IEventHandler<THandler>.RegisterToClient(DiscordSocketClient client, IServiceProvider services) => client.InviteCreated += services.GetRequiredService<THandler>().Execute;
}

