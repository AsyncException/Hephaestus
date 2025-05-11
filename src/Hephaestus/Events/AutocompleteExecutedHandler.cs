
using System;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Hephaestus.Events;

public abstract partial class AutocompleteExecutedHandler<THandler> : IEventHandler<THandler> where THandler : AutocompleteExecutedHandler<THandler> {

	public abstract Task Execute(SocketAutocompleteInteraction arg0);

	static GatewayIntents[] IEventHandler<THandler>.RequiredIntents { get; } = [ GatewayIntents.None ];

	static void IEventHandler<THandler>.RegisterToClient(DiscordSocketClient client, IServiceProvider services) => client.AutocompleteExecuted += services.GetRequiredService<THandler>().Execute;
}

