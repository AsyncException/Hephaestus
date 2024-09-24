namespace Hephaestus.Models;
public record DiscordConfiguration(string Token, string Server, bool SingleServerMode, bool SkipEventIntentCheck);