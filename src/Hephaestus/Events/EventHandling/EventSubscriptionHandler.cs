using Discord;
using Discord.WebSocket;
using Hephaestus.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Immutable;
using System.Reflection;

namespace Hephaestus.Events.EventHandling;

public sealed class EventSubscriptionHandler(DiscordSocketClient client, IServiceProvider services, HephaestusConfiguration config)
{
    private readonly DiscordSocketClient client = client;
    private readonly IServiceProvider services = services;
    private readonly HephaestusConfiguration config = config;

    public Task InitializeAsync() {
        ImmutableArray<ServiceAccessor> accessors = GetEventHandlers(services);

        foreach (ServiceAccessor accessor in accessors) {
            ValidateIntents(config, accessor);
            Bind(accessor, client, services, accessor.ServiceKey, EventExecution);
        }

        return Task.CompletedTask;
    }

    private static void ValidateIntents(HephaestusConfiguration config, ServiceAccessor accessor) {
        if (!config.SkipEventIntentCheck && !accessor.Intents.Any(e => config.GatewayIntentsFlags.HasFlag(e))) {
            throw new Exception($"Event subscriber found for event {accessor.Attribute.EventType} but required intents are not present. Require one of: {string.Join(',', accessor.Intents)}");
        }
    }

    private static void Bind(ServiceAccessor accessor, DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {
        MethodInfo method = accessor.Implementation.GetMethod("Bind", BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy) ?? throw new InvalidOperationException();
        method.Invoke(null, [client, services, key, execution]);
    }

    private static async Task EventExecution(DiscordSocketClient client, IServiceProvider services, Guid key, IEventParameters parameters) {
        using IServiceScope scope = services.CreateScope();
        IEventHandler handler = scope.ServiceProvider.GetRequiredKeyedService<IEventHandler>(key);
        handler.PrepareContext(client, parameters);
        await handler.Execute();
    }

    private static ImmutableArray<ServiceAccessor> GetEventHandlers(IServiceProvider provider) {
        object rootProvider = (provider.GetType().GetProperty("RootProvider", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(provider)) ?? throw new InvalidOperationException();
        object callSiteFactory = rootProvider.GetType().GetProperty("CallSiteFactory", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(rootProvider) ?? throw new InvalidOperationException();
        ServiceDescriptor[] serviceDescriptors = callSiteFactory.GetType().GetProperty("Descriptors", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(callSiteFactory) as ServiceDescriptor[] ?? throw new InvalidOperationException();
        return serviceDescriptors
            .Where(static x => x.ServiceType == typeof(IEventHandler))
            .Select(static x => x.KeyedImplementationType)
            .Where(static x => x is not null)
            .Select(static x => new ServiceAccessor(x!, x?.GetCustomAttribute<EventHandlerAttribute>()!))
            .ToImmutableArray();
    }
}

internal record ServiceAccessor(Type Implementation, EventHandlerAttribute Attribute)
{
    public Guid ServiceKey => Implementation.GUID;
    public GatewayIntents[] Intents => Attribute.Intent;
}