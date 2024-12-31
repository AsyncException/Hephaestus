using Discord;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("AutoModActionExecuted", GatewayIntents.AutoModerationActionExecution)]
public abstract class AutoModActionExecutedHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected AutoModActionExecutedParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (AutoModActionExecutedParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {
        client.AutoModActionExecuted += (arg1, arg2, arg3) => execution(client, services, key, new AutoModActionExecutedParameters(arg1, arg2, arg3));
    }
}

public record AutoModActionExecutedParameters(SocketGuild SocketGuild, AutoModRuleAction AutoModRuleAction, AutoModActionExecutedData AutoModActionExecutedData) : IEventParameters;