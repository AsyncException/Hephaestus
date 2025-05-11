
using System;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Hephaestus.Events;

public abstract partial class CurrentUserUpdatedHandler<THandler> : IEventHandler<THandler> where THandler : CurrentUserUpdatedHandler<THandler> {

	public abstract Task Execute(SocketSelfUser arg0, SocketSelfUser arg1);

	static GatewayIntents[] IEventHandler<THandler>.RequiredIntents { get; } = [ GatewayIntents.None ];

	static void IEventHandler<THandler>.RegisterToClient(DiscordSocketClient client, IServiceProvider services) => client.CurrentUserUpdated += services.GetRequiredService<THandler>().Execute;
}

