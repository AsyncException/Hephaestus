using Discord.WebSocket;
using Hephaestus.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Hephaestus.EventHandling;

public sealed class EventSubscriptionHandler(DiscordSocketClient client, DiscordConfiguration configuration, DiscordSocketConfig discord_socket_config, IServiceProvider services)
{
    private readonly DiscordSocketClient client = client;
    private readonly IServiceProvider services = services;
    private readonly DiscordConfiguration configuration = configuration;
    private readonly DiscordSocketConfig discord_socket_config = discord_socket_config;

    public Task InitiaizeAsync() {
        //TODO: replace the type getting with:
        //new TypeFinder<IEventHandler>().IsAbstract().Inherits<IEventHandler>().HasAttribute<EventHandlerAttribute>().Resolve();
        IEnumerable<Type> event_handlers = typeof(IEventHandler).Assembly.GetTypes().Where(t => t.IsAbstract && t.GetCustomAttribute<EventHandlerAttribute>() != null);
        foreach (Type handler_type in event_handlers) {
            MethodInfo method_info = handler_type.GetRuntimeMethod("MapParameters", [typeof(DiscordSocketClient), typeof(Func<IEventParameters, Task>)]) ?? throw new Exception("Type is missing MapParameters method");
            EventHandlerAttribute attribute = handler_type.GetCustomAttribute<EventHandlerAttribute>() ?? throw new Exception("Type does not have an EventHandlerAttribute");
            Subscribe(attribute, (client, func) => method_info.Invoke(null, [client, func]));
        }

        return Task.CompletedTask;
    }

    private void Subscribe(EventHandlerAttribute event_attribute, Action<DiscordSocketClient, Func<IEventParameters, Task>> subscribe_action) {
        using IServiceScope scope = services.CreateScope();
        IEventHandler[] handlers = scope.ServiceProvider.GetKeyedServices<IEventHandler>(event_attribute.EventType).ToArray();
        if (handlers.Length > 0) {
            if (!configuration.SkipEventIntentCheck && !event_attribute.Intent.Any(e => discord_socket_config.GatewayIntents.HasFlag(e))) {
                throw new Exception($"Event subscriber found for event {event_attribute.EventType} but required intents are not present. Require one of: {string.Join(',', event_attribute.Intent)}");
            }

            subscribe_action(client, (parameters) => Execute(handlers, parameters));
        }
    }

    private async Task Execute(IEventHandler[] handlers, IEventParameters parameters) {
        Task[] tasks = new Task[handlers.Length];
        for (int i = 0; i < handlers.Length; i++) {
            handlers[i].PrepareContext(client, parameters);
            tasks[i] = handlers[i].Execute();
        }

        await Task.WhenAll(tasks);
    }
}