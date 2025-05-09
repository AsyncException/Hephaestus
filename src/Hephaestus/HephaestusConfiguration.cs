using Discord;
using Discord.WebSocket;
using Serilog;

namespace Hephaestus;

public class HephaestusConfiguration : DiscordSocketConfig
{
    public string Token { get; set; } = string.Empty;
    public string Server { get; set; } = string.Empty;
    public bool SingleServerMode { get; set; } = false;
    public bool SkipEventIntentCheck { get; set; } = false;

    public GatewayIntents GatewayIntentsFlags => base.GatewayIntents;

    public new string[] GatewayIntents {
        get => GetGatewayIntent();
        set => SetGatewayIntent(value);
    }

    private void SetGatewayIntent(string[] intents) => base.GatewayIntents = intents.Select(intent => {
            if (!Enum.TryParse(intent, out GatewayIntents parsedIntent)) {
                Log.Logger.Error("Could not parse configured intent: {intent}", intent);
                return Discord.GatewayIntents.None;
            }
            return parsedIntent;

        })
        .Aggregate(Discord.GatewayIntents.None, (combined, intent) => combined | intent);

    private string[] GetGatewayIntent() => Enum.GetValues<GatewayIntents>()
            .Where(intent => base.GatewayIntents.HasFlag(intent))
            .Select(intent => intent.ToString())
            .ToArray();
}