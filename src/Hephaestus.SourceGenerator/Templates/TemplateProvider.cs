namespace Hephaestus.SourceGenerator.Templates;

public static partial class TemplateProvider
{
	public const string JsonEvent = """
		using System;
		using Discord;
		using Discord.Rest;
		using Discord.WebSocket;
		using Newtonsoft.Json.Linq;
		using Microsoft.Extensions.DependencyInjection;
		
		namespace Hephaestus;
		
		public abstract partial class {{ name }}Handler<THandler> : IEventHandler<THandler> where THandler : {{ name }}Handler<THandler> {
		
		    public abstract Task Execute({{ parameters }});
		
		    static GatewayIntents[] IEventHandler<THandler>.RequiredIntents { get; } = [{{ intents }}];
		
		    static void IEventHandler<THandler>.RegisterToClient(DiscordSocketClient client, IServiceProvider services) => client.{{ name }} += services.GetRequiredService<THandler>().Execute;
		}
		""";
}