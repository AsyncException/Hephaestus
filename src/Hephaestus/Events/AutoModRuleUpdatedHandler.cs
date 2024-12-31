using Discord;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("AutoModRuleUpdated", GatewayIntents.AutoModerationConfiguration)]
public abstract class AutoModRuleUpdatedHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected AutoModRuleUpdatedParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (AutoModRuleUpdatedParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {
        client.AutoModRuleUpdated += (arg1, arg2) => execution(client, services, key, new AutoModRuleUpdatedParameters(arg1, arg2));
    }
}

public record AutoModRuleUpdatedParameters(Cacheable<SocketAutoModRule, ulong> OldSocketAutoModRule, SocketAutoModRule SocketAutoModRule) : IEventParameters;