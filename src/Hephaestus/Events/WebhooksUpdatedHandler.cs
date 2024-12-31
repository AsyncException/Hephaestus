using Discord;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("WebhooksUpdated", GatewayIntents.GuildWebhooks)]
public abstract class WebhooksUpdatedHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected WebhooksUpdatedParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (WebhooksUpdatedParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {

        client.WebhooksUpdated += (arg1, arg2) => execution(client, services, key, new WebhooksUpdatedParameters(arg1, arg2));
    }

}

public record WebhooksUpdatedParameters(SocketGuild SocketGuild, SocketChannel SocketChannel) : IEventParameters;