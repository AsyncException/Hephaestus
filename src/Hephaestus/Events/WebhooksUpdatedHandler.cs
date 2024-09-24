using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("WebhooksUpdated", GatewayIntents.GuildWebhooks)]
public abstract class WebhooksUpdatedHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected WebhooksUpdatedParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (WebhooksUpdatedParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.WebhooksUpdated += (SocketGuild, SocketChannel) => execution(new WebhooksUpdatedParameters(SocketGuild, SocketChannel));
}

public record WebhooksUpdatedParameters(SocketGuild SocketGuild, SocketChannel SocketChannel) : IEventParameters;