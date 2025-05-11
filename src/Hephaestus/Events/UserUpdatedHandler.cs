
using System;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Hephaestus.Events;

public abstract partial class UserUpdatedHandler<THandler> : IEventHandler<THandler> where THandler : UserUpdatedHandler<THandler> {

	public abstract Task Execute(SocketUser arg0, SocketUser arg1);

	static GatewayIntents[] IEventHandler<THandler>.RequiredIntents { get; } = [ GatewayIntents.None ];

	static void IEventHandler<THandler>.RegisterToClient(DiscordSocketClient client, IServiceProvider services) => client.UserUpdated += services.GetRequiredService<THandler>().Execute;
}

