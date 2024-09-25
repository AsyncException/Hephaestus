using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Discord.Rest;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Hephaestus.Models;
using Hephaestus.Extensions;

namespace Hephaestus.InteractionHandling;

public sealed class InteractionHandler(
    DiscordSocketClient client,
    IServiceProvider service_provider,
    HephaestusConfiguration configuration,
    ILogger<InteractionHandler> logger,
    InteractionService interaction_service,
    IEnumerable<IAssemblyProvider> assembly_providers
)
{
    private readonly DiscordSocketClient client = client;
    private readonly ILogger<InteractionHandler> logger = logger;
    private readonly HephaestusConfiguration configuration = configuration;
    private readonly IServiceProvider service_provider = service_provider;
    private readonly InteractionService interaction_service = interaction_service;
    private readonly IEnumerable<IAssemblyProvider> assembly_providers = assembly_providers;

    /// <summary>
    /// Initialize the <see cref="InteractionHandler"/> and setup modules registered as <see cref="IAssemblyProvider"/>
    /// </summary>
    /// <returns></returns>
    public async Task InitializeAsync() {
        client.Ready += ReadyAsync;
        interaction_service.Log += logger.LogAsync;
        foreach (IAssemblyProvider provider in assembly_providers) {
            using (IServiceScope scope = service_provider.CreateScope()) {
                await interaction_service.AddModulesAsync(provider.Assembly, scope.ServiceProvider);
            }
        }

        client.InteractionCreated += HandleInteraction;
    }

    /// <summary>
    /// Runs at startup in the <see cref="InitializeAsync"/> and is responsible for registering commands to a guild or globally
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    private async Task ReadyAsync() {
        if (configuration.SingleServerMode) {
            if (ulong.TryParse(configuration.Server, out ulong server)) {
                await interaction_service.RegisterCommandsToGuildAsync(server, true);
                logger.LogDebug("Registered commands to guild {guild_id}", server);
                return;
            }

            logger.LogError("Unable to convert server id to ulong. {configuration_string}", configuration.Server);
            throw new Exception($"Unable to convert server id to ulong. {configuration.Server}");
        }
        else {
            await interaction_service.RegisterCommandsGloballyAsync(true);
            logger.LogDebug("Registered commands globally");
            return;
        }
    }

    /// <summary>
    /// Handles the destination of incoming interaction and makes sure it gets delivered to the correct handler.
    /// </summary>
    /// <param name="interaction"></param>
    /// <returns></returns>
    private async Task HandleInteraction(SocketInteraction interaction) {
        try {
            SocketInteractionContext ctx = new(client, interaction);
            await interaction_service.ExecuteCommandAsync(ctx, service_provider);
        }
        catch {
            if (interaction.Type is InteractionType.ApplicationCommand) {
                RestInteractionMessage original_response = await interaction.GetOriginalResponseAsync();
                await original_response.DeleteAsync();
            }
        }
    }
}