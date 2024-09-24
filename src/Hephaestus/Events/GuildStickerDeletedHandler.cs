using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("GuildStickerDeleted", GatewayIntents.None)]
public abstract class GuildStickerDeletedHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected GuildStickerDeletedParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (GuildStickerDeletedParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.GuildStickerDeleted += (SocketCustomSticker) => execution(new GuildStickerDeletedParameters(SocketCustomSticker));
}

public record GuildStickerDeletedParameters(SocketCustomSticker SocketCustomSticker) : IEventParameters;