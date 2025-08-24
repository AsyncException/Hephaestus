using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;

namespace Hephaestus;

/// <summary>
/// Extention class for mapping the LogServerities to the LogLevels from serilog.
/// </summary>
public static class LoggerExtensions
{
    /// <summary>
    /// Maps the LogSeverity to LogLevel
    /// </summary>
    /// <param name="severity"></param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
    private static LogLevel MapLogLevel(this LogSeverity severity) => severity switch {
        LogSeverity.Critical => LogLevel.Critical,
        LogSeverity.Error => LogLevel.Error,
        LogSeverity.Warning => LogLevel.Warning,
        LogSeverity.Info => LogLevel.Information,
        LogSeverity.Verbose => LogLevel.Trace,
        LogSeverity.Debug => LogLevel.Debug,
        _ => throw new NotSupportedException($"The LogSeverity {severity} is not currently supported"),
    };

    public static Task LogAsync<T>(this ILogger<T> logger, LogMessage message) {
        logger.Log(message.Severity.MapLogLevel(), message.Exception, "[{Source}] {Message}", message.Source, message.Message);
        return Task.CompletedTask;
    }
}