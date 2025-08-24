
using System;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Hephaestus.Events;

public abstract partial class MessageUpdatedHandler<THandler> : IEventHandler<THandler> where THandler : MessageUpdatedHandler<THandler> {

	public abstract Task Execute(Cacheable<IMessage, UInt64> arg0, SocketMessage arg1, ISocketMessageChannel arg2);

	static GatewayIntents[] IEventHandler<THandler>.RequiredIntents { get; } = [ GatewayIntents.None ];

	static void IEventHandler<THandler>.RegisterToClient(DiscordSocketClient client, IServiceProvider services) => client.MessageUpdated += services.GetRequiredService<THandler>().Execute;
}

