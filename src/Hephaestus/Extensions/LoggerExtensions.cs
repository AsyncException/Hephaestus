using Discord;
using Discord.WebSocket;
using Hephaestus.InteractionHandling;
using Microsoft.Extensions.Logging;

namespace Hephaestus.Extensions;

public static class LoggerExtensions
{
    private static LogLevel ConvertToLogLevel(this LogSeverity severity) => severity switch {
        LogSeverity.Critical => LogLevel.Critical,
        LogSeverity.Error => LogLevel.Error,
        LogSeverity.Warning => LogLevel.Warning,
        LogSeverity.Info => LogLevel.Information,
        LogSeverity.Verbose => LogLevel.Trace,
        LogSeverity.Debug => LogLevel.Debug,
        _ => throw new NotSupportedException($"The LogSeverity {severity} is not currently supported"),
    };

    public static async Task LogAsync(this ILogger<DiscordSocketClient> logger, LogMessage message) {
        logger.Log(message.Severity.ConvertToLogLevel(), message.Exception, "[{Source}] {Message}", message.Source, message.Message);
        await Task.CompletedTask;
    }

    public static async Task LogAsync(this ILogger<InteractionHandler> logger, LogMessage message) {
        logger.Log(message.Severity.ConvertToLogLevel(), message.Exception, "[{Source}] {Message}", message.Source, message.Message);
        await Task.CompletedTask;
    }
}