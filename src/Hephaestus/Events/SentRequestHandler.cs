
using System;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Hephaestus.Events;

public abstract partial class SentRequestHandler<THandler> : IEventHandler<THandler> where THandler : SentRequestHandler<THandler> {

	public abstract Task Execute(String arg0, String arg1, Double arg2);

	static GatewayIntents[] IEventHandler<THandler>.RequiredIntents { get; } = [ GatewayIntents.None ];

	static void IEventHandler<THandler>.RegisterToClient(DiscordSocketClient client, IServiceProvider services) => client.SentRequest += services.GetRequiredService<THandler>().Execute;
}

