
using System;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Hephaestus.Events;

public abstract partial class GuildStickerCreatedHandler<THandler> : IEventHandler<THandler> where THandler : GuildStickerCreatedHandler<THandler> {

	public abstract Task Execute(SocketCustomSticker arg0);

	static GatewayIntents[] IEventHandler<THandler>.RequiredIntents { get; } = [ GatewayIntents.None ];

	static void IEventHandler<THandler>.RegisterToClient(DiscordSocketClient client, IServiceProvider services) => client.GuildStickerCreated += services.GetRequiredService<THandler>().Execute;
}

