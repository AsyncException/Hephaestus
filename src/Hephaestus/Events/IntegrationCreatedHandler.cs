
using System;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Hephaestus.Events;

public abstract partial class IntegrationCreatedHandler<THandler> : IEventHandler<THandler> where THandler : IntegrationCreatedHandler<THandler> {

	public abstract Task Execute(IIntegration arg0);

	static GatewayIntents[] IEventHandler<THandler>.RequiredIntents { get; } = [ GatewayIntents.None ];

	static void IEventHandler<THandler>.RegisterToClient(DiscordSocketClient client, IServiceProvider services) => client.IntegrationCreated += services.GetRequiredService<THandler>().Execute;
}

