
using System;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Hephaestus.Events;

public abstract partial class SpeakerRemovedHandler<THandler> : IEventHandler<THandler> where THandler : SpeakerRemovedHandler<THandler> {

	public abstract Task Execute(SocketStageChannel arg0, SocketGuildUser arg1);

	static GatewayIntents[] IEventHandler<THandler>.RequiredIntents { get; } = [ GatewayIntents.None ];

	static void IEventHandler<THandler>.RegisterToClient(DiscordSocketClient client, IServiceProvider services) => client.SpeakerRemoved += services.GetRequiredService<THandler>().Execute;
}

