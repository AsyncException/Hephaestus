using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Hephaestus;

/// <summary>
/// A record holding the information about the handler for an interaction so it can be added to the <see cref="DiscordSocketClient"/> from the <see cref="IServiceProvider"/>
/// </summary>
/// <param name="Type"></param>
public record InteractionHandlerReference(Type Type);

/// <summary>
/// Extention class for the <see cref="IServiceCollection"/> to make adding EventHandlers easier.
/// </summary>
public static class HephaestusInteractionHandlerExtensions
{
    /// <summary>
    /// Adds an InteractionHandler to the <see cref="IServiceCollection"/>
    /// </summary>
    /// <typeparam name="T">The handler to add</typeparam>
    /// <param name="services">The services which to add the handler to</param>
    /// <returns>The <see cref="IServiceCollection"/> for chaining</returns>
    public static IServiceCollection AddInteractionHandler<T>(this IServiceCollection services) where T : IInteractionModuleBase =>
        services.AddTransient(services => new InteractionHandlerReference(typeof(T)));
}

/// <summary>
/// A handler for subscribing <see cref="IInteractionModuleBase"/> to the discord events.
/// </summary>
public interface IInteractionHandler
{
    public Task InitializeAsync();
}

/// <summary>
/// The base implementation of the <see cref="IInteractionHandler"/>
/// </summary>
/// <param name="client"></param>
/// <param name="service_provider"></param>
/// <param name="configuration"></param>
/// <param name="logger"></param>
/// <param name="interaction_service"></param>
public sealed class InteractionHandler(
    DiscordSocketClient client,
    IServiceProvider service_provider,
    HephaestusConfiguration configuration,
    ILogger<InteractionHandler> logger,
    InteractionService interaction_service
) : IInteractionHandler
{
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
            await interaction_service.ExecuteCommandAsync(new SocketInteractionContext(client, interaction), service_provider);
        }
        catch when (interaction.Type is InteractionType.ApplicationCommand) {
            await (await interaction.GetOriginalResponseAsync()).DeleteAsync();
        }
    }

    /// <summary>
    /// Initialized single server mode, registering the commands only to the server selected in the config
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    private async Task InitializeSingleServerMode() {
        await interaction_service.RegisterCommandsToGuildAsync(configuration.Server, true);
        logger.LogDebug("Registered commands to guild {guild_id}", configuration.Server);
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