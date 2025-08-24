
using System;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Hephaestus.Events;

public abstract partial class PresenceUpdatedHandler<THandler> : IEventHandler<THandler> where THandler : PresenceUpdatedHandler<THandler> {

	public abstract Task Execute(SocketUser arg0, SocketPresence arg1, SocketPresence arg2);

	static GatewayIntents[] IEventHandler<THandler>.RequiredIntents { get; } = [ GatewayIntents.None ];

	static void IEventHandler<THandler>.RegisterToClient(DiscordSocketClient client, IServiceProvider services) => client.PresenceUpdated += services.GetRequiredService<THandler>().Execute;
}

