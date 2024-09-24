using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("JoinedGuild", GatewayIntents.None)]
public abstract class JoinedGuildHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected JoinedGuildParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (JoinedGuildParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.JoinedGuild += (SocketGuild) => execution(new JoinedGuildParameters(SocketGuild));
}

public record JoinedGuildParameters(SocketGuild SocketGuild) : IEventParameters;