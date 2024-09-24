using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("AutoModRuleCreated", GatewayIntents.AutoModerationConfiguration)]
public abstract class AutoModRuleCreatedHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected AutoModRuleCreatedParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (AutoModRuleCreatedParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.AutoModRuleCreated += (SocketAutoModRule) => execution(new AutoModRuleCreatedParameters(SocketAutoModRule));
}

public record AutoModRuleCreatedParameters(SocketAutoModRule SocketAutoModRule) : IEventParameters;