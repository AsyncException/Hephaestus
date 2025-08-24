using Discord;
using Discord.WebSocket;

namespace Hephaestus.Events;

public record EventHandlerReference(string HandlerName, Action<DiscordSocketClient, IServiceProvider> RegistrationFunction, GatewayIntents[] RequiredIntent);