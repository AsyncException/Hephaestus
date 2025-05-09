using Discord;
using Discord.WebSocket;

namespace Hephaestus.Events;

[EventHandler(["socketMessage"],[typeof(SocketMessage)],[GatewayIntents.GuildMessages, GatewayIntents.MessageContent])]
public abstract partial class MessageReceivedHandler<THandler> : IEventHandler<THandler>;

[EventHandler(["socketAuditLogEntry", "socketGuild"], [typeof(SocketAuditLogEntry), typeof(SocketGuild)],[GatewayIntents.None])]
public abstract partial class AuditLogCreatedHandler<THandler> : IEventHandler<THandler>;