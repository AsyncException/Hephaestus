using Discord;
using Discord.Interactions;
using Discord.Rest;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Hephaestus.Interactions;

public sealed class InteractionHandler(
    DiscordSocketClient client,
    IServiceProvider service_provider,
    HephaestusConfiguration configuration,
    ILogger<InteractionHandler> logger,
    InteractionService interaction_service
)
{
    private readonly DiscordSocketClient client = client;
    private readonly ILogger<InteractionHandler> logger = logger;
    private readonly HephaestusConfiguration configuration = configuration;
    private readonly IServiceProvider service_provider = service_provider;
    private readonly InteractionService interaction_service = interaction_service;

    /// <summary>
    /// Initialize the <see cref="InteractionHandler"/> and setup modules registered as <see cref="IAssemblyProvider"/>
    /// </summary>
    /// <returns></returns>
    public async Task InitializeAsync() {
        client.Ready += ReadyAsync;
        interaction_service.Log += logger.LogAsync;
        client.InteractionCreated += HandleInteraction;
        await InitializeModules();
    }

    /// <summary>
    /// Runs at startup in the <see cref="InitializeAsync"/> and is responsible for registering commands to a guild or globally
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    private async Task ReadyAsync() => await (configuration.SingleServerMode ? InitializeSingleServerMode() : InitializeGlobalServerMode());

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

    /// <summary>
    /// Initialized single server mode, registering the commands only to the server selected in the config
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    private async Task InitializeSingleServerMode() {
        if (!ulong.TryParse(configuration.Server, out ulong server)) {
            logger.LogError("Unable to convert server id to ulong. {configuration_string}", configuration.Server);
            throw new Exception($"Unable to convert server id to ulong. {configuration.Server}");
        }

        await interaction_service.RegisterCommandsToGuildAsync(server, true);
        logger.LogDebug("Registered commands to guild {guild_id}", server);
        return;
    }

    /// <summary>
    /// Initialized global server mode, registering the commands globally
    /// </summary>
    /// <returns></returns>
    private async Task InitializeGlobalServerMode() {
        await interaction_service.RegisterCommandsGloballyAsync(true);
        logger.LogDebug("Registered commands globally");
        return;
    }

    /// <summary>
    /// Registers all assembly providers with the interaction service
    /// </summary>
    /// <returns></returns>
    private async Task InitializeModules() {
        foreach (InteractionHandlerReference interaction in service_provider.GetRequiredService<IEnumerable<InteractionHandlerReference>>()) {
            await interaction_service.AddModuleAsync(interaction.Type, service_provider);
        }
    }
}