using Discord.WebSocket;

namespace Hephaestus.Models;

public class HephaestusConfiguration : DiscordSocketConfig
{
    public string Token { get; set; } = string.Empty;
    public string Server { get; set; } = string.Empty;
    public bool SingleServerMode { get; set; } = false;
    public bool SkipEventIntentCheck { get; set; } = false;
}