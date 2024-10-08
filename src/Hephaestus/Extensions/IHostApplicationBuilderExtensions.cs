﻿using Discord.Commands;
using Discord.Interactions;
using Discord.Rest;
using Discord.WebSocket;
using Hephaestus.EventHandling;
using Hephaestus.Extensions;
using Hephaestus.InteractionHandling;
using Hephaestus.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using System.Reflection;

namespace Hephaestus;

public static class IHostApplicationBuilderExtensions
{
    /// <summary>
    /// Add required settings and services for Hephaestus to work correctly. To register modules use <seealso cref="AddHephaestusModule{T}"/>
    /// </summary>
    /// <remarks>
    ///		Actions this method performs:
    ///		<list type="bullet">
    ///			<item><description>Setup the configuration to use the appsettings.json file and if debugging also adds the user secrets</description></item>
    ///			<item><description>Adds Serilog as logging provider and apply default overrides and formatting.</description></item>
    ///			<item><description>Add the basic required services to the DI container</description></item>
    ///		</list>
    /// </remarks>
    ///
    /// <param name="builder"></param>
    /// <returns></returns>
    public static IHostApplicationBuilder AddHephaestus(this IHostApplicationBuilder builder, Action<DiscordSocketConfig>? configurationSetup = null) {
        builder.ConfigureLogging();
        builder.ConfigureServices();

        //Add configuration

        builder.Services.AddTransient<HephaestusConfiguration>((services) => {
            HephaestusConfiguration config = services.GetRequiredService<IConfiguration>().GetSection("Hephaestus").Get<HephaestusConfiguration>() ?? new();
            configurationSetup?.Invoke(config);
            return config;
        });

        return builder;
    }

    /// <summary>
    /// Adds Serilog as logging provider and apply default overrides and formatting.
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static IHostApplicationBuilder ConfigureLogging(this IHostApplicationBuilder builder) {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Warning)
            .MinimumLevel.Override("System.Net.Http.HttpClient", LogEventLevel.Warning)
            .WriteTo.Debug(restrictedToMinimumLevel: LogEventLevel.Debug, outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3} {SourceContext}] {Message:lj}{NewLine}{Exception}")
            .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Information, outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
            .Enrich.FromLogContext()
            .CreateLogger();

        builder.Logging.AddSerilog();
        return builder;
    }

    /// <summary>
    /// Add the basic required services to the DI container and allows extra services to be registrated via the <paramref name="additional_services"/>
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="additional_services">Action that allows additional services to be registered</param>
    /// <returns></returns>
    private static IHostApplicationBuilder ConfigureServices(this IHostApplicationBuilder builder) {
        builder.Services.AddSerilog();
        builder.Services.AddHostedService<BootStrapper>();
        builder.Services.AddSingleton(provider => {
            HephaestusConfiguration config = provider.GetRequiredService<HephaestusConfiguration>();
            ILogger<DiscordSocketClient> logger = provider.GetRequiredService<ILogger<DiscordSocketClient>>();
            DiscordSocketClient client = new(config);

            client.Log += logger.LogAsync;

            return client;
        });
        builder.Services.AddSingleton<DiscordRestClient>(e => e.GetRequiredService<DiscordSocketClient>().Rest);
        builder.Services.AddSingleton<InteractionService>();
        builder.Services.AddSingleton<InteractionHandler>();
        builder.Services.AddSingleton<EventSubscriptionHandler>();
        builder.Services.AddSingleton<CommandService>();

        return builder;
    }

    /// <summary>
    /// Register <see cref="IAssemblyProvider"/> using this method.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="host_builder"></param>
    /// <returns></returns>
    public static IHostApplicationBuilder AddHephaestusModule<T>(this IHostApplicationBuilder host_builder) where T : class, IAssemblyProvider, new() {
        new T().OptionalModules(host_builder);
        host_builder.Services.AddSingleton<IAssemblyProvider, T>();

        foreach (Type eventHandler in new TypeFinder<T>().IsNotAbstract().Inherits<IEventHandler>().HasAttribute<EventHandlerAttribute>().Resolve()) {
            EventHandlerAttribute attribute = eventHandler.GetCustomAttribute<EventHandlerAttribute>() ?? throw new Exception("Event attribute not found.");
            host_builder.Services.AddTransient(typeof(IEventHandler), eventHandler);
            host_builder.Services.AddKeyedTransient(typeof(IEventHandler), attribute.EventType, eventHandler);
        }

        return host_builder;
    }
}