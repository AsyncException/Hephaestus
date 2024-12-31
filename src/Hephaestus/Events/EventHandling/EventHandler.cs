using Discord.WebSocket;

namespace Hephaestus.Events.EventHandling;

public interface IEventHandler
{
    public Task Execute();
    public abstract void PrepareContext(DiscordSocketClient client, IEventParameters parameters);
}

public interface IEventParameters;
