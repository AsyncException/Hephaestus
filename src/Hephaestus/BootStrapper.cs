using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Hephaestus;

/// <summary>
/// The main background process thats keeping the bot alive
/// </summary>
/// <param name="client"></param>
/// <param name="eventHandler"></param>
/// <param name="logger"></param>
/// <param name="configuration"></param>
/// <param name="interactionHandler"></param>
internal sealed class BootStrapper(
    DiscordSocketClient client,
    ILogger<BootStrapper> logger,
    HephaestusConfiguration configuration,
    IEventSubscriptionHandler eventHandler,
    IInteractionHandler interactionHandler
    ) : IHostedService
{
    /// <summary>
    /// Starts up and logs in the discord client
    /// </summary>
    /// <param name="cancellation_token"></param>
    /// <returns></returns>
    public async Task StartAsync(CancellationToken cancellation_token) {
        logger.LogDebug("Bootstrapper started");

        //Subscribing event handlers to the events.
        eventHandler.InitializeAsync();
        
        //Subscribing interactions
        await interactionHandler.InitializeAsync();

        await client.LoginAsync(TokenType.Bot, configuration.Token);
        await client.StartAsync();
    }

    /// <summary>
    /// Stops the discord client
    /// </summary>
    /// <param name="cancellation_token"></param>
    /// <returns></returns>
    public async Task StopAsync(CancellationToken cancellation_token) {
        logger.LogDebug("Bootstrapper stopped");
        await client.StopAsync();
    }
}