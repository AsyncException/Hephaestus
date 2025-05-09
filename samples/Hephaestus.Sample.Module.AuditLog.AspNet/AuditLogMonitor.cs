using Discord;
using Discord.WebSocket;
using Hephaestus.Events;
using Hephaestus.Sample.Module.AuditLog.AspNet.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Hephaestus.Sample.Module.AuditLog.AspNet;

public class AuditLogMonitor(ILogger<AuditLogMonitor> logger, DatabaseContext database, DiscordSocketClient client) : AuditLogCreatedHandler<AuditLogMonitor>
{
    public async override Task Execute(SocketAuditLogEntry SocketAuditLogEntry, SocketGuild SocketGuild) {
        logger.LogDebug("[auditlog created] received audit log created event");
        AuditLogConfiguration? config = await database.AuditLogConfigurations.Where(config => config.Server == SocketGuild.Id).FirstOrDefaultAsync();

        if (config is null) {
            return;
        }

        IChannel channel = await client.GetChannelAsync(config.ChannelId);
        if (channel is not ITextChannel text_channel) {
            logger.LogError("[auditlog created] Channel with id {channel_id} was not of type ITextChannel. Guild: {guild_id}", config.ChannelId, SocketGuild.Id);
            return;
        }

        await text_channel.SendMessageAsync(embed: new EmbedBuilder()
            .WithTitle("AuditLog")
            .WithDescription(SocketAuditLogEntry.Reason)
            .WithFields(new EmbedFieldBuilder().WithName("Action").WithValue(SocketAuditLogEntry.Action).WithIsInline(true))
            .WithFooter($"{SocketAuditLogEntry.CreatedAt} - {SocketAuditLogEntry.User.Username}")
            .Build());
    }
}