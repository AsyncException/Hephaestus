using Discord.Interactions;
using Hephaestus.Events;
using Hephaestus.Interactions;
using Microsoft.Extensions.DependencyInjection;

namespace Hephaestus;

public static class HephaestusModuleExtensions {
    public static IServiceCollection AddInteractionHandler<T>(this IServiceCollection services) where T : IInteractionModuleBase =>
        services.AddTransient(services => new InteractionHandlerReference(typeof(T)));

    public static IServiceCollection AddInteractionHandler(this IServiceCollection services, Type type) {
        if(!typeof(IInteractionModuleBase).IsAssignableFrom(type)) {
            throw new ArgumentException($"Type {type.Name} does not implement {nameof(IInteractionModuleBase)}");
        }

        services.AddTransient(services => new InteractionHandlerReference(type));

        return services;
    }

    public static IServiceCollection AddEventHandler<T>(this IServiceCollection services) where T : IEventHandler<T> =>
        services.AddTransient(services => new EventHandlerReference(nameof(T), T.RegisterToClient, T.RequiredIntents));
}
