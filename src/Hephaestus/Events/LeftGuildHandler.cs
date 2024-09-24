using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("LeftGuild", GatewayIntents.None)]
public abstract class LeftGuildHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected LeftGuildParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (LeftGuildParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.LeftGuild += (SocketGuild) => execution(new LeftGuildParameters(SocketGuild));
}

public record LeftGuildParameters(SocketGuild SocketGuild) : IEventParameters;