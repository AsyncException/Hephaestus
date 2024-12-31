using Discord;
using Discord.WebSocket;
using Hephaestus.Events.EventHandling;

namespace Hephaestus;

//TODO: Add documentation and intents check
[EventHandler("RoleUpdated", GatewayIntents.Guilds)]
public abstract class RoleUpdatedHandler : IEventHandler
{
    protected DiscordSocketClient Client { get; private set; } = default!;
    protected RoleUpdatedParameters Context { get; private set; } = default!;

    public abstract Task Execute();
    public void PrepareContext(DiscordSocketClient client, IEventParameters parameters) {
        Client = client;
        Context = (RoleUpdatedParameters)parameters;
    }

    public static void Bind(DiscordSocketClient client, IServiceProvider services, Guid key, Func<DiscordSocketClient, IServiceProvider, Guid, IEventParameters, Task> execution) {

        client.RoleUpdated += (arg1, arg2) => execution(client, services, key, new RoleUpdatedParameters(arg1, arg2));
    }

}

public record RoleUpdatedParameters(SocketRole OldSocketRole, SocketRole SocketRole) : IEventParameters;