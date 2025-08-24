using Discord.Interactions;
using Hephaestus.Events;
using Hephaestus.Interactions;
using Microsoft.Extensions.DependencyInjection;

namespace Hephaestus;

public static class HephaestusModuleExtensions {
    /// <summary>
    /// Adds an InteractionHandler to the <see cref="IServiceCollection"/>
    /// </summary>
    /// <typeparam name="T">The handler to add</typeparam>
    /// <param name="services">The services which to add the handler to</param>
    /// <returns>The <see cref="IServiceCollection"/> for chaining</returns>
    public static IServiceCollection AddInteractionHandler<T>(this IServiceCollection services) where T : IInteractionModuleBase =>
        services.AddTransient(services => new InteractionHandlerReference(typeof(T)));

    /// <summary>
    /// Adds an InteractionHandler to the <see cref="IServiceCollection"/>
    /// </summary>
    /// <typeparam name="T">The handler to add</typeparam>
    /// <param name="services">The services which to add the handler to</param>
    /// <returns>The <see cref="IServiceCollection"/> for chaining</returns>
    public static IServiceCollection AddInteractionHandler(this IServiceCollection services, Type type) {
        if(!typeof(IInteractionModuleBase).IsAssignableFrom(type)) {
            throw new ArgumentException($"Type {type.Name} does not implement {nameof(IInteractionModuleBase)}");
        }

        services.AddTransient(services => new InteractionHandlerReference(type));

        return services;
    }

    public static IServiceCollection AddEventHandler<T>(this IServiceCollection services) where T : IEventHandler<T> => services.AddTransient(services => new EventHandlerReference(nameof(T), T.RegisterToClient, T.RequiredIntents));
}
