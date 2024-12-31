using Discord;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("AutoModRuleCreated", GatewayIntents.AutoModerationConfiguration)]
public abstract class AutoModRuleCreatedHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected AutoModRuleCreatedParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (AutoModRuleCreatedParameters)parameters;
    }
    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {
        client.AutoModRuleCreated += (arg1) => execution(client, services, key, new AutoModRuleCreatedParameters(arg1));
    }

}

public record AutoModRuleCreatedParameters(SocketAutoModRule SocketAutoModRule) : IEventParameters;