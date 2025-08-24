using Discord;
using Discord.WebSocket;
using Hephaestus.Events;
using Hephaestus.Sample.Module.AuditLog.Hosting.Models;
using Microsoft.Extensions.Logging;

namespace Hephaestus.Sample.Module.AuditLog.Hosting;

public class AuditLogMonitor(ILogger<AuditLogMonitor> logger, ConfigProvider configProvider, DiscordSocketClient client) : AuditLogCreatedHandler<AuditLogMonitor>
{
    public async override Task Execute(SocketAuditLogEntry socketAuditLogEntry, SocketGuild socketGuild) {
        logger.LogDebug("[auditlog created] received audit log created event");
        AuditLogConfiguration? config = configProvider.Config.FirstOrDefault(e => e.Server == socketGuild.Id);

        if (config is null) {
            return;
        }

        IChannel channel = await client.GetChannelAsync(config.ChannelId);
        if (channel is not ITextChannel text_channel) {
            logger.LogError("[auditlog created] Channel with id {channel_id} was not of type ITextChannel. Guild: {guild_id}", config.ChannelId, socketAuditLogEntry.Id);
            return;
        }

        await text_channel.SendMessageAsync(embed: new EmbedBuilder()
            .WithTitle("AuditLog")
            .WithDescription(socketAuditLogEntry.Reason)
            .WithFields(new EmbedFieldBuilder().WithName("Action").WithValue(socketAuditLogEntry.Action).WithIsInline(true))
            .WithFooter($"{socketAuditLogEntry.CreatedAt} - {socketAuditLogEntry.User.Username}")
            .Build());
    }
}