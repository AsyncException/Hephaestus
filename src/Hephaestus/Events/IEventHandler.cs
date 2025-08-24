using Discord;
using Discord.WebSocket;

namespace Hephaestus.Events;

/// <summary>
/// An interface that the event handlers inherit. This is needed to access the <see cref="RegisterToClient(DiscordSocketClient, IServiceProvider)"/> statically in the registration.
/// </summary>
/// <typeparam name="THandler">A reference to the inheriting type</typeparam>
public interface IEventHandler<THandler> where THandler : IEventHandler<THandler>
{
    internal static abstract void RegisterToClient(DiscordSocketClient client, IServiceProvider services);
    public static abstract GatewayIntents[] RequiredIntents { get; }
}
