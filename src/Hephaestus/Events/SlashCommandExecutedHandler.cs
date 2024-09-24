using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using EventHandler = Hephaestus.EventHandling.EventHandler;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("SlashCommandExecuted", GatewayIntents.None)]
public abstract class SlashCommandExecutedHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected SlashCommandExecutedParameters Context { get; private set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (SlashCommandExecutedParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.SlashCommandExecuted += (SocketSlashCommand) => execution(new SlashCommandExecutedParameters(SocketSlashCommand));
}

public record SlashCommandExecutedParameters(SocketSlashCommand SocketSlashCommand) : IEventParameters;