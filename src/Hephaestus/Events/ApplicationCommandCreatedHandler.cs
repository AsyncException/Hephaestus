using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace Hephaestus.Events;

public abstract partial class ApplicationCommandCreatedHandler<THandler> : IEventHandler<THandler> where THandler : ApplicationCommandCreatedHandler<THandler> {

	public abstract Task Execute(SocketApplicationCommand arg0);

	static GatewayIntents[] IEventHandler<THandler>.RequiredIntents { get; } = [ GatewayIntents.None ];

	static void IEventHandler<THandler>.RegisterToClient(DiscordSocketClient client, IServiceProvider services) => client.ApplicationCommandCreated += services.GetRequiredService<THandler>().Execute;
}

