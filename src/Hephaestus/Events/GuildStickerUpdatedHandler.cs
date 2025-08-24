
using System;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Hephaestus.Events;

public abstract partial class GuildStickerUpdatedHandler<THandler> : IEventHandler<THandler> where THandler : GuildStickerUpdatedHandler<THandler> {

	public abstract Task Execute(SocketCustomSticker arg0, SocketCustomSticker arg1);

	static GatewayIntents[] IEventHandler<THandler>.RequiredIntents { get; } = [ GatewayIntents.None ];

	static void IEventHandler<THandler>.RegisterToClient(DiscordSocketClient client, IServiceProvider services) => client.GuildStickerUpdated += services.GetRequiredService<THandler>().Execute;
}

