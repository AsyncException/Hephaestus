
using System;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Hephaestus.Events;

public abstract partial class UserIsTypingHandler<THandler> : IEventHandler<THandler> where THandler : UserIsTypingHandler<THandler> {

	public abstract Task Execute(Cacheable<IUser, UInt64> arg0, Cacheable<IMessageChannel, UInt64> arg1);

	static GatewayIntents[] IEventHandler<THandler>.RequiredIntents { get; } = [ GatewayIntents.None ];

	static void IEventHandler<THandler>.RegisterToClient(DiscordSocketClient client, IServiceProvider services) => client.UserIsTyping += services.GetRequiredService<THandler>().Execute;
}

