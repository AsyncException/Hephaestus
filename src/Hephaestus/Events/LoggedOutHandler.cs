
using System;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Hephaestus.Events;

public abstract partial class LoggedOutHandler<THandler> : IEventHandler<THandler> where THandler : LoggedOutHandler<THandler> {

	public abstract Task Execute();

	static GatewayIntents[] IEventHandler<THandler>.RequiredIntents { get; } = [ GatewayIntents.None ];

	static void IEventHandler<THandler>.RegisterToClient(DiscordSocketClient client, IServiceProvider services) => client.LoggedOut += services.GetRequiredService<THandler>().Execute;
}

