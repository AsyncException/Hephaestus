
using System;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Hephaestus.Events;

public abstract partial class ModalSubmittedHandler<THandler> : IEventHandler<THandler> where THandler : ModalSubmittedHandler<THandler> {

	public abstract Task Execute(SocketModal arg0);

	static GatewayIntents[] IEventHandler<THandler>.RequiredIntents { get; } = [ GatewayIntents.None ];

	static void IEventHandler<THandler>.RegisterToClient(DiscordSocketClient client, IServiceProvider services) => client.ModalSubmitted += services.GetRequiredService<THandler>().Execute;
}

