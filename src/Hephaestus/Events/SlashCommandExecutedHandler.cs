
using System;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Hephaestus.Events;

public abstract partial class SlashCommandExecutedHandler<THandler> : IEventHandler<THandler> where THandler : SlashCommandExecutedHandler<THandler> {

	public abstract Task Execute(SocketSlashCommand arg0);

	static GatewayIntents[] IEventHandler<THandler>.RequiredIntents { get; } = [ GatewayIntents.None ];

	static void IEventHandler<THandler>.RegisterToClient(DiscordSocketClient client, IServiceProvider services) => client.SlashCommandExecuted += services.GetRequiredService<THandler>().Execute;
}

