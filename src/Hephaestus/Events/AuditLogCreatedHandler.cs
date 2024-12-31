using Discord;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("AuditLogCreated", GatewayIntents.None)]
public abstract class AuditLogCreatedHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected AuditLogCreatedParameters Context { get; set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (AuditLogCreatedParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {
        client.AuditLogCreated += (arg1, arg2) => execution(client, services, key, new AuditLogCreatedParameters(arg1, arg2));
    }
}

public record AuditLogCreatedParameters(SocketAuditLogEntry SocketAuditLogEntry, SocketGuild SocketGuild) : IEventParameters;