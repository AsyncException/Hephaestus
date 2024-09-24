using Discord;
using Discord.WebSocket;
using Hephaestus.Sample.Module.AuditLog.AspNet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Hephaestus.Sample.Module.AuditLog.AspNet;

[Route("[controller]/[action]")]
public class AuditLogApi(DiscordSocketClient client, DatabaseContext database, ILogger<AuditLogApi> logger) : Controller
{
    private readonly DiscordSocketClient client = client;
    private readonly DatabaseContext database = database;
    private readonly ILogger<AuditLogApi> logger = logger;

    [HttpPost]
    public async Task<IActionResult> UpdateLogging(ulong guild_id, ulong channel_id) {
        SocketGuild? guild = client.GetGuild(guild_id);
        if (guild is null) {
            return Problem("Given server does not exist or the bot has not joined this server");
        }

        SocketGuildChannel? channel = guild.GetChannel(channel_id);
        if (channel is null) {
            return Problem("Given server does not has a channel with that id");
        }

        if (channel is not ITextChannel text_channel) {
            return Problem("Given channel is not a text channel");
        }

        AuditLogConfiguration? audit_configuration = await database.AuditLogConfigurations.Where(config => config.Server == guild.Id).FirstOrDefaultAsync();

        if (audit_configuration is null) {
            audit_configuration = new() { Server = guild.Id };
            database.Add(audit_configuration);
            logger.LogInformation("[AuditLogApi] Creating new audit config for {server_id} with channel {channel_id}", guild.Id, text_channel.Id);
        }
        else {
            logger.LogInformation("[AuditLogApi] Updating audit config channel from {old_id} to {new_id}", audit_configuration.ChannelId, text_channel.Id);
        }

        audit_configuration.ChannelId = text_channel.Id;

        await database.SaveChangesAsync();

        return Ok($"{text_channel.Name} set as audit log channel");
    }
}