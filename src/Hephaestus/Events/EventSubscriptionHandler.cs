using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace Hephaestus.Events;

public partial class EventSubscriptionHandler(DiscordSocketClient client, IServiceProvider services, HephaestusConfiguration config)
{
    private readonly DiscordSocketClient client = client;
    private readonly IServiceProvider services = services;
    private readonly HephaestusConfiguration config = config;

    public void InitializeAsync() {
        foreach(EventHandlerReference handlerReference in services.GetRequiredService<IEnumerable<EventHandlerReference>>()) {
            if(!handlerReference.RequiredIntent.Any(intent => config.GatewayIntentsFlags.HasFlag(intent))) {
                
            }

            handlerReference.RegistrationFunction(client, services);
        }
    }
}