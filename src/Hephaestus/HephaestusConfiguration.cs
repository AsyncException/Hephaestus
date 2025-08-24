using Discord;
using Discord.WebSocket;
using Serilog;

namespace Hephaestus;

/// <summary>
/// An extention of the <see cref="DiscordSocketConfig"/> with additional properties for Hephaestus to work.
/// </summary>
public class HephaestusConfiguration : DiscordSocketConfig
{
    /// <summary>
    /// The token for the bot to use.
    /// </summary>
    public string Token { get; set; } = string.Empty;

    /// <summary>
    /// The ID of the server. Only required when <see cref="SingleServerMode"/> is enabled.
    /// </summary>
    public ulong Server { get; set; } = 0L;

    /// <summary>
    /// If the bot is only meant to be run on a single server. Make sure the <see cref="Server"/> is filled in.
    /// </summary>
    public bool SingleServerMode { get; set; } = false;

    /// <summary>
    /// If checking if the correct intents are set for the event handlers that are registered.
    /// </summary>
    public bool SkipEventIntentCheck { get; set; } = false;

    /// <summary>
    /// The combination of all intents together as flags.
    /// </summary>
    public GatewayIntents GatewayIntentsFlags => base.GatewayIntents;

    /// <summary>
    /// The assigned intents as an array.
    /// </summary>
    public new string[] GatewayIntents {
        get {
            return [.. Enum.GetValues<GatewayIntents>()
                .Where(intent => base.GatewayIntents.HasFlag(intent))
                .Select(intent => intent.ToString())];
        }
        set {
            base.GatewayIntents = value
                .Select(Enum.Parse<GatewayIntents>)
                .Aggregate(Discord.GatewayIntents.None, (combined, intent) => combined | intent);
        }
    }
}