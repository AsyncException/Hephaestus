using Discord;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;


namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("AutoModRuleDeleted", GatewayIntents.AutoModerationConfiguration)]
public abstract class AutoModRuleDeletedHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected AutoModRuleDeletedParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (AutoModRuleDeletedParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {
        client.AutoModRuleDeleted += (arg1) => execution(client, services, key, new AutoModRuleDeletedParameters(arg1));
    }
}

public record AutoModRuleDeletedParameters(SocketAutoModRule SocketAutoModRule) : IEventParameters;