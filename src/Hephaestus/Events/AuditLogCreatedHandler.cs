using Discord.WebSocket;
using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventHandler = Hephaestus.EventHandling.EventHandler;
using Hephaestus.EventHandling;

namespace Hephaestus.Events;

//TODO: Add documentation and intents check
[EventHandler("AuditLogCreated", GatewayIntents.None)]
public abstract class AuditLogCreatedHandler : EventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected AuditLogCreatedParameters Context { get; set; } = default!;

    public override void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (AuditLogCreatedParameters)parameters;
    }

    public static void MapParameters(DiscordSocketClient client, Func<IEventParameters, Task> execution) =>
        client.AuditLogCreated += (SocketAuditLog, SocketGuild) => execution(new AuditLogCreatedParameters(SocketAuditLog, SocketGuild));
}

public record AuditLogCreatedParameters(SocketAuditLogEntry SocketAuditLogEntry, SocketGuild SocketGuild) : IEventParameters;