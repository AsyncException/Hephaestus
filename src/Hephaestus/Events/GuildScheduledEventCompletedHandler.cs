using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("GuildScheduledEventCompleted", GatewayIntents.None)]
public abstract class GuildScheduledEventCompletedHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected GuildScheduledEventCompletedParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (GuildScheduledEventCompletedParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.GuildScheduledEventCompleted += (SocketGuildEvent) => execution(new GuildScheduledEventCompletedParameters(SocketGuildEvent));
}

public record GuildScheduledEventCompletedParameters(SocketGuildEvent SocketGuildEvent) : IEventParameters;