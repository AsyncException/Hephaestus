using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("AutoModActionExecuted", GatewayIntents.AutoModerationActionExecution)]
public abstract class AutoModActionExecutedHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected AutoModActionExecutedParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (AutoModActionExecutedParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.AutoModActionExecuted += (MessageDeleted, AutoModRuleAction, AutoModActionExecutedData) => execution(new AutoModActionExecutedParameters(MessageDeleted, AutoModRuleAction, AutoModActionExecutedData));
}

public record AutoModActionExecutedParameters(SocketGuild SocketGuild, AutoModRuleAction AutoModRuleAction, AutoModActionExecutedData AutoModActionExecutedData) : IEventParameters;