using Discord;
using System.Diagnostics.CodeAnalysis;

namespace Hephaestus.Events
{
    [AttributeUsage(AttributeTargets.Class)]
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
#pragma warning disable CS9113 // Parameter is unread.
    public class EventHandlerAttribute(string[] parameterNames, Type[] parameters, GatewayIntents[] intent) : Attribute;
#pragma warning restore CS9113 // Parameter is unread.
}