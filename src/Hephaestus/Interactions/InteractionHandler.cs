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
    private readonly DiscordSocketClient _client = client;
    private readonly ILogger<InteractionHandler> _logger = logger;
    private readonly IServiceProvider _serviceProvider = service_provider;
    private readonly HephaestusConfiguration _configuration = configuration;
    private readonly InteractionService _interactionService = interaction_service;

    /// <summary>
    /// Initialize the <see cref="InteractionHandler"/> and setup modules registered as <see cref="IAssemblyProvider"/>
    /// </summary>
    /// <returns></returns>
    public async Task InitializeAsync() {
        _client.Ready += ReadyAsync;
        _interactionService.Log += _logger.LogAsync;
        _client.InteractionCreated += HandleInteraction;
        await InitializeModules();
    }

    /// <summary>
    /// Runs at startup in the <see cref="InitializeAsync"/> and is responsible for registering commands to a guild or globally
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    private async Task ReadyAsync() => await (_configuration.SingleServerMode ? InitializeSingleServerMode() : InitializeGlobalServerMode());

    /// <summary>
    /// Handles the destination of incoming interaction and makes sure it gets delivered to the correct handler.
    /// </summary>
    /// <param name="interaction"></param>
    /// <returns></returns>
    private async Task HandleInteraction(SocketInteraction interaction) {
        try {
            SocketInteractionContext ctx = new(_client, interaction);
            await _interactionService.ExecuteCommandAsync(ctx, _serviceProvider);
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
        await _interactionService.RegisterCommandsToGuildAsync(_configuration.Server, true);
        _logger.LogDebug("Registered commands to guild {guild_id}", _configuration.Server);
        return;
    }

    /// <summary>
    /// Initialized global server mode, registering the commands globally
    /// </summary>
    /// <returns></returns>
    private async Task InitializeGlobalServerMode() {
        await _interactionService.RegisterCommandsGloballyAsync(true);
        _logger.LogDebug("Registered commands globally");
        return;
    }

    /// <summary>
    /// Registers all assembly providers with the interaction service
    /// </summary>
    /// <returns></returns>
    private async Task InitializeModules() {
        foreach (InteractionHandlerReference interaction in _serviceProvider.GetRequiredService<IEnumerable<InteractionHandlerReference>>()) {
            await _interactionService.AddModuleAsync(interaction.Type, _serviceProvider);
        }
    }
}