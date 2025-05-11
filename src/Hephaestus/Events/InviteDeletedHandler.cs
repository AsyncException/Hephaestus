
using System;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Hephaestus.Events;

public abstract partial class InviteDeletedHandler<THandler> : IEventHandler<THandler> where THandler : InviteDeletedHandler<THandler> {

	public abstract Task Execute(SocketGuildChannel arg0, String arg1);

	static GatewayIntents[] IEventHandler<THandler>.RequiredIntents { get; } = [ GatewayIntents.None ];

	static void IEventHandler<THandler>.RegisterToClient(DiscordSocketClient client, IServiceProvider services) => client.InviteDeleted += services.GetRequiredService<THandler>().Execute;
}

