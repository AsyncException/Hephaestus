using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace Hephaestus;

/// <summary>
/// An interface that the event handlers inherit. This is needed to access the <see cref="RegisterToClient(DiscordSocketClient, IServiceProvider)"/> statically in the registration.
/// </summary>
/// <typeparam name="THandler">A reference to the inheriting type</typeparam>
public interface IEventHandler<THandler> where THandler : IEventHandler<THandler>
{
    internal static abstract void RegisterToClient(DiscordSocketClient client, IServiceProvider services);
    public static abstract GatewayIntents[] RequiredIntents { get; }
}

/// <summary>
/// A record holding the information about the handler for an event so it van be subscribed to the <see cref="DiscordSocketClient"/> from the <see cref="IServiceProvider"/>
/// </summary>
/// <param name="HandlerName"></param>
/// <param name="RegistrationFunction"></param>
/// <param name="RequiredIntent"></param>
public record EventHandlerReference(string HandlerName, Action<DiscordSocketClient, IServiceProvider> RegistrationFunction, GatewayIntents[] RequiredIntent);

/// <summary>
/// Extension class for the <see cref="IServiceCollection"/> to make adding EventHandlers easier.
/// </summary>
public static class HephaestusEventHandlerExtensions
{
    /// <summary>
    /// Adds an EventHandler to the <see cref="IServiceCollection"/>
    /// </summary>
    /// <typeparam name="T">The handler to add</typeparam>
    /// <param name="services">The services which to add the handler to</param>
    /// <returns>The <see cref="IServiceCollection"/> for chaining</returns>
    public static IServiceCollection AddEventHandler<T>(this IServiceCollection services) where T : IEventHandler<T> =>
        services.AddTransient(services => new EventHandlerReference(nameof(T), T.RegisterToClient, T.RequiredIntents));
}

/// <summary>
/// A handler for subscribing <see cref="IEventHandler{THandler}"/> to the discord events.
/// </summary>
public interface IEventSubscriptionHandler {
    /// <summary>
    /// Initializes the EventSubscriptionHandler
    /// </summary>
    public void InitializeAsync();
}

/// <summary>
/// The base implementation of the <see cref="IEventSubscriptionHandler"/>
/// </summary>
/// <param name="client">The <see cref="DiscordSocketClient"/> from the service provider</param>
/// <param name="services">The <see cref="IServiceProvider"/> for creating handlers</param>
/// <param name="config">The configuration containing the intents present for the <see cref="DiscordSocketClient"/></param>
public class EventSubscriptionHandler(DiscordSocketClient client, IServiceProvider services, HephaestusConfiguration config) : IEventSubscriptionHandler
{
    /// <summary>
    /// Subscribes the EventHandlers to the client.
    /// </summary>
    /// <exception cref="Exception">Throws when an event is used that is missing an intent</exception>
    public void InitializeAsync() {
        foreach (EventHandlerReference handlerReference in services.GetRequiredService<IEnumerable<EventHandlerReference>>()) {
            if(!handlerReference.RequiredIntent.Any(intent => config.GatewayIntentsFlags.HasFlag(intent))) {
                throw new Exception($"Event handler: {handlerReference.HandlerName} has missing requirements. This handler need one of {string.Join(", ", handlerReference.RequiredIntent)}");                
            }

            handlerReference.RegistrationFunction(client, services);
        }
    }
}