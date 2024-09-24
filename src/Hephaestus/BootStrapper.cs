using Discord;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using Hephaestus.InteractionHandling;
using Hephaestus.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Hephaestus;

internal sealed class BootStrapper(
    DiscordSocketClient client,
    EventSubscriptionHandler event_handler,
    ILogger<BootStrapper> logger,
    DiscordConfiguration configuration,
    InteractionHandler interaction_handler
) : IHostedService
{
    private readonly DiscordSocketClient client = client;
    private readonly ILogger<BootStrapper> logger = logger;
    private readonly EventSubscriptionHandler event_handler = event_handler;
    private readonly DiscordConfiguration configuration = configuration;
    private readonly InteractionHandler interaction_handler = interaction_handler;

    public async Task StartAsync(CancellationToken cancellation_token) {
        logger.LogDebug("Bootstrapper started");

        await event_handler.InitiaizeAsync();
        await interaction_handler.InitializeAsync();

        await client.LoginAsync(TokenType.Bot, configuration.Token);
        await client.StartAsync();
    }

    public async Task StopAsync(CancellationToken cancellation_token) {
        logger.LogDebug("Bootstrapper stopped");
        await client.StopAsync();
    }
}