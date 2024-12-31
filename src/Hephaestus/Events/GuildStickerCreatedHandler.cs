using Discord;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("GuildStickerCreated", GatewayIntents.None)]
public abstract class GuildStickerCreatedHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected GuildStickerCreatedParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (GuildStickerCreatedParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {

        client.GuildStickerCreated += (arg1) => execution(client, services, key, new GuildStickerCreatedParameters(arg1));
    }

}

public record GuildStickerCreatedParameters(SocketCustomSticker SocketCustomSticker) : IEventParameters;