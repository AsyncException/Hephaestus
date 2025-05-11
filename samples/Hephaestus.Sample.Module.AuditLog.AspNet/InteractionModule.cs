using Discord;
using Discord.Interactions;
using Hephaestus.Sample.Module.AuditLog.AspNet.Models;
using Microsoft.Extensions.Logging;

namespace Hephaestus.Sample.Module.AuditLog.AspNet;

[Group("setup", "server setup")]
public class InteractionModule(ConfigProvider configProvider, ILogger<InteractionModule> logger) : InteractionModuleBase<SocketInteractionContext>
{
    [SlashCommand("audit", "setup audit logging")]
    public async Task SetupAuditlogs([Summary(description: "Text to quote")] ITextChannel channel) {
        await DeferAsync(ephemeral: true);

        try {
            logger.LogDebug("[setup audit] Update audit config, Server: {server_name} {server_id}, Channel: {channe_name} {channel_id}", Context.Guild.Name, Context.Guild.Id, channel.Name, channel.Id);
            AuditLogConfiguration? config = configProvider.Config.FirstOrDefault(e => e.Server == Context.Guild.Id);

            if (config is null) {
                config = new() { Server = Context.Guild.Id };
                configProvider.Config.Add(config);
                logger.LogInformation("[setup audit] Creating new audit config for {server_id} with channel {channel_id}", Context.Guild.Id, channel.Id);
            }
            else {
                logger.LogInformation("[setup audit] Updating audit config channel from {old_id} to {new_id}", config.ChannelId, channel.Id);
            }

            config.ChannelId = channel.Id;

            await FollowupAsync($"{channel.Mention} set as audit log channel");
        }
        catch (Exception ex) {
            logger.LogError(ex, "[setup audit] Something went wrong setting up an audit log channel");
            await FollowupAsync($"Something went wrong while setting the audit log channel. Please try again later.");
        }
    }
}