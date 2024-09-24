using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("GuildStickerCreated", GatewayIntents.None)]
public abstract class GuildStickerCreatedHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected GuildStickerCreatedParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (GuildStickerCreatedParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.GuildStickerCreated += (SocketCustomSticker) => execution(new GuildStickerCreatedParameters(SocketCustomSticker));
}

public record GuildStickerCreatedParameters(SocketCustomSticker SocketCustomSticker) : IEventParameters;