
using System;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Hephaestus.Events;

public abstract partial class ThreadMemberLeftHandler<THandler> : IEventHandler<THandler> where THandler : ThreadMemberLeftHandler<THandler> {

	public abstract Task Execute(SocketThreadUser arg0);

	static GatewayIntents[] IEventHandler<THandler>.RequiredIntents { get; } = [ GatewayIntents.None ];

	static void IEventHandler<THandler>.RegisterToClient(DiscordSocketClient client, IServiceProvider services) => client.ThreadMemberLeft += services.GetRequiredService<THandler>().Execute;
}

