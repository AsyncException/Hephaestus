using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("AutoModRuleUpdated", GatewayIntents.AutoModerationConfiguration)]
public abstract class AutoModRuleUpdatedHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected AutoModRuleUpdatedParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (AutoModRuleUpdatedParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.AutoModRuleUpdated += (OldSocketAutoModRule, SocketAutoModRule) => execution(new AutoModRuleUpdatedParameters(OldSocketAutoModRule, SocketAutoModRule));
}

public record AutoModRuleUpdatedParameters(Cacheable<SocketAutoModRule, ulong> OldSocketAutoModRule, SocketAutoModRule SocketAutoModRule) : IEventParameters;