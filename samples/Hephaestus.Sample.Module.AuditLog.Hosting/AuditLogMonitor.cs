using Discord;
using Hephaestus.Events;
using Hephaestus.Sample.Module.AuditLog.Hosting.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Hephaestus.Sample.Module.AuditLog.Hosting;

public class AuditLogMonitor(ILogger<AuditLogMonitor> logger, DatabaseContext database) : AuditLogCreatedHandler
{
    public override async Task Execute() {
        logger.LogDebug("[auditlog created] received audit log created event");
        AuditLogConfiguration? config = await database.AuditLogConfigurations.Where(config => config.Server == Context.SocketGuild.Id).FirstOrDefaultAsync();

        if (config is null) {
            return;
        }

        IChannel channel = await Client.GetChannelAsync(config.ChannelId);
        if (channel is not ITextChannel text_channel) {
            logger.LogError("[auditlog created] Channel with id {channel_id} was not of type ITextChannel. Guild: {guild_id}", config.ChannelId, Context.SocketGuild.Id);
            return;
        }

        await text_channel.SendMessageAsync(embed: new EmbedBuilder()
            .WithTitle("AuditLog")
            .WithDescription(Context.SocketAuditLogEntry.Reason)
            .WithFields(new EmbedFieldBuilder().WithName("Action").WithValue(Context.SocketAuditLogEntry.Action).WithIsInline(true))
            .WithFooter($"{Context.SocketAuditLogEntry.CreatedAt} - {Context.SocketAuditLogEntry.User.Username}")
            .Build());
    }
}