
using System;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Hephaestus.Events;

public abstract partial class StageStartedHandler<THandler> : IEventHandler<THandler> where THandler : StageStartedHandler<THandler> {

	public abstract Task Execute(SocketStageChannel arg0);

	static GatewayIntents[] IEventHandler<THandler>.RequiredIntents { get; } = [ GatewayIntents.None ];

	static void IEventHandler<THandler>.RegisterToClient(DiscordSocketClient client, IServiceProvider services) => client.StageStarted += services.GetRequiredService<THandler>().Execute;
}

