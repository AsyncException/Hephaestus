using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("AutoModRuleDeleted", GatewayIntents.AutoModerationConfiguration)]
public abstract class AutoModRuleDeletedHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected AutoModRuleDeletedParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (AutoModRuleDeletedParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.AutoModRuleDeleted += (SocketAutoModRule) => execution(new AutoModRuleDeletedParameters(SocketAutoModRule));
}

public record AutoModRuleDeletedParameters(SocketAutoModRule SocketAutoModRule) : IEventParameters;