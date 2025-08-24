using Discord;
using Discord.WebSocket;

namespace Hephaestus.Events;

public interface IEventHandler<THandler> where THandler : IEventHandler<THandler>
{
    internal static abstract void RegisterToClient(DiscordSocketClient client, IServiceProvider services);
    public static abstract GatewayIntents[] RequiredIntents { get; }
}
