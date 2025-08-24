
using System;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Hephaestus.Events;

public abstract partial class UserVoiceStateUpdatedHandler<THandler> : IEventHandler<THandler> where THandler : UserVoiceStateUpdatedHandler<THandler> {

	public abstract Task Execute(SocketUser arg0, SocketVoiceState arg1, SocketVoiceState arg2);

	static GatewayIntents[] IEventHandler<THandler>.RequiredIntents { get; } = [ GatewayIntents.None ];

	static void IEventHandler<THandler>.RegisterToClient(DiscordSocketClient client, IServiceProvider services) => client.UserVoiceStateUpdated += services.GetRequiredService<THandler>().Execute;
}

