
using System;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Hephaestus.Events;

public abstract partial class ReactionsRemovedForEmoteHandler<THandler> : IEventHandler<THandler> where THandler : ReactionsRemovedForEmoteHandler<THandler> {

	public abstract Task Execute(Cacheable<IUserMessage, UInt64> arg0, Cacheable<IMessageChannel, UInt64> arg1, IEmote arg2);

	static GatewayIntents[] IEventHandler<THandler>.RequiredIntents { get; } = [ GatewayIntents.None ];

	static void IEventHandler<THandler>.RegisterToClient(DiscordSocketClient client, IServiceProvider services) => client.ReactionsRemovedForEmote += services.GetRequiredService<THandler>().Execute;
}

