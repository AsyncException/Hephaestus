
using System;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Hephaestus.Events;

public abstract partial class ApplicationCommandDeletedHandler<THandler> : IEventHandler<THandler> where THandler : ApplicationCommandDeletedHandler<THandler> {

	public abstract Task Execute(SocketApplicationCommand arg0);

	static GatewayIntents[] IEventHandler<THandler>.RequiredIntents { get; } = [ GatewayIntents.None ];

	static void IEventHandler<THandler>.RegisterToClient(DiscordSocketClient client, IServiceProvider services) => client.ApplicationCommandDeleted += services.GetRequiredService<THandler>().Execute;
}

