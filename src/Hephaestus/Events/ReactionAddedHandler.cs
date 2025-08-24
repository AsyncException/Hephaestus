
using System;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Hephaestus.Events;

public abstract partial class ReactionAddedHandler<THandler> : IEventHandler<THandler> where THandler : ReactionAddedHandler<THandler> {

	public abstract Task Execute(Cacheable<IUserMessage, UInt64> arg0, Cacheable<IMessageChannel, UInt64> arg1, SocketReaction arg2);

	static GatewayIntents[] IEventHandler<THandler>.RequiredIntents { get; } = [ GatewayIntents.None ];

	static void IEventHandler<THandler>.RegisterToClient(DiscordSocketClient client, IServiceProvider services) => client.ReactionAdded += services.GetRequiredService<THandler>().Execute;
}

