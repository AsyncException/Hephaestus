
using System;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Hephaestus.Events;

public abstract partial class VoiceChannelStatusUpdatedHandler<THandler> : IEventHandler<THandler> where THandler : VoiceChannelStatusUpdatedHandler<THandler> {

	public abstract Task Execute(Cacheable<SocketVoiceChannel, UInt64> arg0, String arg1, String arg2);

	static GatewayIntents[] IEventHandler<THandler>.RequiredIntents { get; } = [ GatewayIntents.None ];

	static void IEventHandler<THandler>.RegisterToClient(DiscordSocketClient client, IServiceProvider services) => client.VoiceChannelStatusUpdated += services.GetRequiredService<THandler>().Execute;
}

