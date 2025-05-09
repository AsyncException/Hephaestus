using Discord.Commands;
using Discord.Interactions;
using Discord.Rest;
using Discord.WebSocket;
using Hephaestus.Events;
using Hephaestus.Interactions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace Hephaestus;
#pragma warning restore IDE0130 // Namespace does not match folder structure

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
    /// <param name="builder">The <seealso cref="IHostApplicationBuilder"/> to register the services to</param>
    /// <param name="configurationSetup">An after initialization configuration modifier. Overrides settings from the <seealso cref="IConfiguration"/></param>
    /// <param name="configurationSection">Optional: a custom section key. If not null the key is required to exist</param>
    /// <returns>The original <seealso cref="IHostApplicationBuilder"/> for chaining</returns>
    /// <exception cref="InvalidOperationException">Gets thrown if the <paramref name="configurationSection"/> is set but was not found</exception>
    /// <exception cref="NullReferenceException">Gets thrown if the <paramref name="configurationSection"/> is set and the key was found but contained no values or incorrect values</exception>
    public static IHostApplicationBuilder AddHephaestus(this IHostApplicationBuilder builder, Action<HephaestusConfiguration> configurationSetup, string? configurationSection = null) {
        builder.ConfigureLogging();
        builder.ConfigureServices();

        //Add configuration
        builder.Services.AddTransient((IServiceProvider services) => {
            IConfiguration configuration = services.GetRequiredService<IConfiguration>();

            HephaestusConfiguration config = null!;
            if (configurationSection is null) {
                IConfigurationSection section = configuration.GetSection("Hephaestus");
                config = section.Get<HephaestusConfiguration>() ?? new();
            }
            else {
                //If a user gives a configuration section key it should always return a configuration because its expected to be there.
                IConfigurationSection section = configuration.GetRequiredSection(configurationSection);
                config = section.Get<HephaestusConfiguration>()
                    ?? throw new NullReferenceException($"The configuration section with key \"{configurationSection}\" return a null configuration after deserializing.");
            }

            configurationSetup.Invoke(config);
            return config;
        });

        return builder;
    }

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
    /// <param name="builder">The <seealso cref="IHostApplicationBuilder"/> to register the services to</param>
    /// <param name="configurationSection">Optional: a custom section key. If not null the key is required to exist</param>
    /// <returns>The original <seealso cref="IHostApplicationBuilder"/> for chaining</returns>
    /// <exception cref="InvalidOperationException">Gets thrown if the <paramref name="configurationSection"/> is set but was not found</exception>
    /// <exception cref="NullReferenceException">Gets thrown if the <paramref name="configurationSection"/> is set and the key was found but contained no values or incorrect values</exception>
    public static IHostApplicationBuilder AddHephaestus(this IHostApplicationBuilder builder, string? configurationSection = null) => AddHephaestus(builder, static config => { }, configurationSection);

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
        builder.Services.AddSingleton(static provider => {
            HephaestusConfiguration config = provider.GetRequiredService<HephaestusConfiguration>();
            ILogger<DiscordSocketClient> logger = provider.GetRequiredService<ILogger<DiscordSocketClient>>();
            DiscordSocketClient client = new(config);

            client.Log += logger.LogAsync;

            return client;
        });
        builder.Services.AddSingleton<DiscordRestClient>(static e => e.GetRequiredService<DiscordSocketClient>().Rest);
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
    public static IHostApplicationBuilder AddHephaestusModule<T>(this IHostApplicationBuilder host_builder) where T : IHephaestusModule<T>, new() {
        T.RegisterServices(host_builder.Configuration, host_builder.Services);
        return host_builder;
    }
}