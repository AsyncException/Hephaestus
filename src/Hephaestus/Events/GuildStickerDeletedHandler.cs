using Discord;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("GuildStickerDeleted", GatewayIntents.None)]
public abstract class GuildStickerDeletedHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected GuildStickerDeletedParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (GuildStickerDeletedParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {

        client.GuildStickerDeleted += (arg1) => execution(client, services, key, new GuildStickerDeletedParameters(arg1));
    }

}

public record GuildStickerDeletedParameters(SocketCustomSticker SocketCustomSticker) : IEventParameters;