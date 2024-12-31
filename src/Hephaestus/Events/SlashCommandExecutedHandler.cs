using Discord;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("SlashCommandExecuted", GatewayIntents.None)]
public abstract class SlashCommandExecutedHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected SlashCommandExecutedParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (SlashCommandExecutedParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {

        client.SlashCommandExecuted += (arg1) => execution(client, services, key, new SlashCommandExecutedParameters(arg1));
    }

}

public record SlashCommandExecutedParameters(SocketSlashCommand SocketSlashCommand) : IEventParameters;