using Discord;

namespace Hephaestus.Events
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
#pragma warning disable CS9113 // Parameter is unread.
    public class EventHandlerAttribute(Type[] parameters, GatewayIntents[] intent) : Attribute;
#pragma warning restore CS9113 // Parameter is unread.
}