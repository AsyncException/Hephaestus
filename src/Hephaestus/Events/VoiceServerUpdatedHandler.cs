
using System;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Hephaestus.Events;

public abstract partial class VoiceServerUpdatedHandler<THandler> : IEventHandler<THandler> where THandler : VoiceServerUpdatedHandler<THandler> {

	public abstract Task Execute(SocketVoiceServer arg0);

	static GatewayIntents[] IEventHandler<THandler>.RequiredIntents { get; } = [ GatewayIntents.None ];

	static void IEventHandler<THandler>.RegisterToClient(DiscordSocketClient client, IServiceProvider services) => client.VoiceServerUpdated += services.GetRequiredService<THandler>().Execute;
}

