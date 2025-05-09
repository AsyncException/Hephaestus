using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;

///Extracted using the following "script"
//EventInfo[] events = typeof(BaseSocketClient).GetEvents(BindingFlags.Public | BindingFlags.Instance);
//foreach (EventInfo eventInfo in events.OrderBy(e => e.Name)) {
//    string parametersTypes = string.Join(", ", eventInfo.EventHandlerType!.GetMethod("Invoke")!.GetParameters().Select(e => $"typeof({GetFullType(e.ParameterType)})"));

//    string details = $$"""
//        [EventHandler([{{parametersTypes}}], [GatewayIntents.None])]
//        public abstract partial class {{eventInfo.Name}}Handler<THandler>;

//    """;

//    Console.WriteLine(details);
//}

//string GetFullType(Type type) {
//    return type.IsGenericType
//        ? $"{type.Name.Substring(0, type.Name.Length - 2)}<{string.Join(", ", type.GenericTypeArguments.Select(e => GetFullType(e)))}>"
//        : type.Name;
//}

namespace Hephaestus.Events;

[EventHandler([typeof(SocketApplicationCommand)], [GatewayIntents.None])]
public abstract partial class ApplicationCommandCreatedHandler<THandler>;

[EventHandler([typeof(SocketApplicationCommand)], [GatewayIntents.None])]
public abstract partial class ApplicationCommandDeletedHandler<THandler>;

[EventHandler([typeof(SocketApplicationCommand)], [GatewayIntents.None])]
public abstract partial class ApplicationCommandUpdatedHandler<THandler>;

[EventHandler([typeof(SocketAuditLogEntry), typeof(SocketGuild)], [GatewayIntents.None])]
public abstract partial class AuditLogCreatedHandler<THandler>;

[EventHandler([typeof(SocketAutocompleteInteraction)], [GatewayIntents.None])]
public abstract partial class AutocompleteExecutedHandler<THandler>;

[EventHandler([typeof(SocketGuild), typeof(AutoModRuleAction), typeof(AutoModActionExecutedData)], [GatewayIntents.None])]
public abstract partial class AutoModActionExecutedHandler<THandler>;

[EventHandler([typeof(SocketAutoModRule)], [GatewayIntents.None])]
public abstract partial class AutoModRuleCreatedHandler<THandler>;

[EventHandler([typeof(SocketAutoModRule)], [GatewayIntents.None])]
public abstract partial class AutoModRuleDeletedHandler<THandler>;

[EventHandler([typeof(Cacheable<SocketAutoModRule, UInt64>), typeof(SocketAutoModRule)], [GatewayIntents.None])]
public abstract partial class AutoModRuleUpdatedHandler<THandler>;

[EventHandler([typeof(SocketMessageComponent)], [GatewayIntents.None])]
public abstract partial class ButtonExecutedHandler<THandler>;

[EventHandler([typeof(SocketChannel)], [GatewayIntents.None])]
public abstract partial class ChannelCreatedHandler<THandler>;

[EventHandler([typeof(SocketChannel)], [GatewayIntents.None])]
public abstract partial class ChannelDestroyedHandler<THandler>;

[EventHandler([typeof(SocketChannel), typeof(SocketChannel)], [GatewayIntents.None])]
public abstract partial class ChannelUpdatedHandler<THandler>;

[EventHandler([typeof(SocketSelfUser), typeof(SocketSelfUser)], [GatewayIntents.None])]
public abstract partial class CurrentUserUpdatedHandler<THandler>;

[EventHandler([typeof(SocketEntitlement)], [GatewayIntents.None])]
public abstract partial class EntitlementCreatedHandler<THandler>;

[EventHandler([typeof(Cacheable<SocketEntitlement, UInt64>)], [GatewayIntents.None])]
public abstract partial class EntitlementDeletedHandler<THandler>;

[EventHandler([typeof(Cacheable<SocketEntitlement, UInt64>), typeof(SocketEntitlement)], [GatewayIntents.None])]
public abstract partial class EntitlementUpdatedHandler<THandler>;

[EventHandler([typeof(SocketGuild)], [GatewayIntents.None])]
public abstract partial class GuildAvailableHandler<THandler>;

[EventHandler([typeof(Cacheable<SocketGuildUser, UInt64>), typeof(SocketGuild)], [GatewayIntents.None])]
public abstract partial class GuildJoinRequestDeletedHandler<THandler>;

[EventHandler([typeof(SocketGuild)], [GatewayIntents.None])]
public abstract partial class GuildMembersDownloadedHandler<THandler>;

[EventHandler([typeof(Cacheable<SocketGuildUser, UInt64>), typeof(SocketGuildUser)], [GatewayIntents.None])]
public abstract partial class GuildMemberUpdatedHandler<THandler>;

[EventHandler([typeof(SocketGuildEvent)], [GatewayIntents.None])]
public abstract partial class GuildScheduledEventCancelledHandler<THandler>;

[EventHandler([typeof(SocketGuildEvent)], [GatewayIntents.None])]
public abstract partial class GuildScheduledEventCompletedHandler<THandler>;

[EventHandler([typeof(SocketGuildEvent)], [GatewayIntents.None])]
public abstract partial class GuildScheduledEventCreatedHandler<THandler>;

[EventHandler([typeof(SocketGuildEvent)], [GatewayIntents.None])]
public abstract partial class GuildScheduledEventStartedHandler<THandler>;

[EventHandler([typeof(Cacheable<SocketGuildEvent, UInt64>), typeof(SocketGuildEvent)], [GatewayIntents.None])]
public abstract partial class GuildScheduledEventUpdatedHandler<THandler>;

[EventHandler([typeof(Cacheable<SocketUser, RestUser, IUser, UInt64>), typeof(SocketGuildEvent)], [GatewayIntents.None])]
public abstract partial class GuildScheduledEventUserAddHandler<THandler>;

[EventHandler([typeof(Cacheable<SocketUser, RestUser, IUser, UInt64>), typeof(SocketGuildEvent)], [GatewayIntents.None])]
public abstract partial class GuildScheduledEventUserRemoveHandler<THandler>;

[EventHandler([typeof(SocketCustomSticker)], [GatewayIntents.None])]
public abstract partial class GuildStickerCreatedHandler<THandler>;

[EventHandler([typeof(SocketCustomSticker)], [GatewayIntents.None])]
public abstract partial class GuildStickerDeletedHandler<THandler>;

[EventHandler([typeof(SocketCustomSticker), typeof(SocketCustomSticker)], [GatewayIntents.None])]
public abstract partial class GuildStickerUpdatedHandler<THandler>;

[EventHandler([typeof(SocketGuild)], [GatewayIntents.None])]
public abstract partial class GuildUnavailableHandler<THandler>;

[EventHandler([typeof(SocketGuild), typeof(SocketGuild)], [GatewayIntents.None])]
public abstract partial class GuildUpdatedHandler<THandler>;

[EventHandler([typeof(IIntegration)], [GatewayIntents.None])]
public abstract partial class IntegrationCreatedHandler<THandler>;

[EventHandler([typeof(IGuild), typeof(UInt64), typeof(Optional<UInt64>)], [GatewayIntents.None])]
public abstract partial class IntegrationDeletedHandler<THandler>;

[EventHandler([typeof(IIntegration)], [GatewayIntents.None])]
public abstract partial class IntegrationUpdatedHandler<THandler>;

[EventHandler([typeof(SocketInteraction)], [GatewayIntents.None])]
public abstract partial class InteractionCreatedHandler<THandler>;

[EventHandler([typeof(SocketInvite)], [GatewayIntents.None])]
public abstract partial class InviteCreatedHandler<THandler>;

[EventHandler([typeof(SocketGuildChannel), typeof(String)], [GatewayIntents.None])]
public abstract partial class InviteDeletedHandler<THandler>;

[EventHandler([typeof(SocketGuild)], [GatewayIntents.None])]
public abstract partial class JoinedGuildHandler<THandler>;

[EventHandler([typeof(SocketGuild)], [GatewayIntents.None])]
public abstract partial class LeftGuildHandler<THandler>;

[EventHandler([typeof(LogMessage)], [GatewayIntents.None])]
public abstract partial class LogHandler<THandler>;

[EventHandler([], [GatewayIntents.None])]
public abstract partial class LoggedInHandler<THandler>;

[EventHandler([], [GatewayIntents.None])]
public abstract partial class LoggedOutHandler<THandler>;

[EventHandler([typeof(SocketMessageCommand)], [GatewayIntents.None])]
public abstract partial class MessageCommandExecutedHandler<THandler>;

[EventHandler([typeof(Cacheable<IMessage, UInt64>), typeof(Cacheable<IMessageChannel, UInt64>)], [GatewayIntents.None])]
public abstract partial class MessageDeletedHandler<THandler>;

[EventHandler([typeof(SocketMessage)], [GatewayIntents.None])]
public abstract partial class MessageReceivedHandler<THandler>;

[EventHandler([typeof(IReadOnlyCollection<Cacheable<IMessage, UInt64>>), typeof(Cacheable<IMessageChannel, UInt64>)], [GatewayIntents.None])]
public abstract partial class MessagesBulkDeletedHandler<THandler>;

[EventHandler([typeof(Cacheable<IMessage, UInt64>), typeof(SocketMessage), typeof(ISocketMessageChannel)], [GatewayIntents.None])]
public abstract partial class MessageUpdatedHandler<THandler>;

[EventHandler([typeof(SocketModal)], [GatewayIntents.None])]
public abstract partial class ModalSubmittedHandler<THandler>;

[EventHandler([typeof(Cacheable<IUser, UInt64>), typeof(Cacheable<ISocketMessageChannel, IRestMessageChannel, IMessageChannel, UInt64>), typeof(Cacheable<IUserMessage, UInt64>), typeof(Nullable<Cacheable<SocketGuild, RestGuild, IGuild, UInt64>>), typeof(UInt64)], [GatewayIntents.None])]
public abstract partial class PollVoteAddedHandler<THandler>;

[EventHandler([typeof(Cacheable<IUser, UInt64>), typeof(Cacheable<ISocketMessageChannel, IRestMessageChannel, IMessageChannel, UInt64>), typeof(Cacheable<IUserMessage, UInt64>), typeof(Nullable<Cacheable<SocketGuild, RestGuild, IGuild, UInt64>>), typeof(UInt64)], [GatewayIntents.None])]
public abstract partial class PollVoteRemovedHandler<THandler>;

[EventHandler([typeof(SocketUser), typeof(SocketPresence), typeof(SocketPresence)], [GatewayIntents.None])]
public abstract partial class PresenceUpdatedHandler<THandler>;

[EventHandler([typeof(Cacheable<IUserMessage, UInt64>), typeof(Cacheable<IMessageChannel, UInt64>), typeof(SocketReaction)], [GatewayIntents.None])]
public abstract partial class ReactionAddedHandler<THandler>;

[EventHandler([typeof(Cacheable<IUserMessage, UInt64>), typeof(Cacheable<IMessageChannel, UInt64>), typeof(SocketReaction)], [GatewayIntents.None])]
public abstract partial class ReactionRemovedHandler<THandler>;

[EventHandler([typeof(Cacheable<IUserMessage, UInt64>), typeof(Cacheable<IMessageChannel, UInt64>)], [GatewayIntents.None])]
public abstract partial class ReactionsClearedHandler<THandler>;

[EventHandler([typeof(Cacheable<IUserMessage, UInt64>), typeof(Cacheable<IMessageChannel, UInt64>), typeof(IEmote)], [GatewayIntents.None])]
public abstract partial class ReactionsRemovedForEmoteHandler<THandler>;

[EventHandler([typeof(SocketGroupUser)], [GatewayIntents.None])]
public abstract partial class RecipientAddedHandler<THandler>;

[EventHandler([typeof(SocketGroupUser)], [GatewayIntents.None])]
public abstract partial class RecipientRemovedHandler<THandler>;

[EventHandler([typeof(SocketStageChannel), typeof(SocketGuildUser)], [GatewayIntents.None])]
public abstract partial class RequestToSpeakHandler<THandler>;

[EventHandler([typeof(SocketRole)], [GatewayIntents.None])]
public abstract partial class RoleCreatedHandler<THandler>;

[EventHandler([typeof(SocketRole)], [GatewayIntents.None])]
public abstract partial class RoleDeletedHandler<THandler>;

[EventHandler([typeof(SocketRole), typeof(SocketRole)], [GatewayIntents.None])]
public abstract partial class RoleUpdatedHandler<THandler>;

[EventHandler([typeof(SocketMessageComponent)], [GatewayIntents.None])]
public abstract partial class SelectMenuExecutedHandler<THandler>;

[EventHandler([typeof(String), typeof(String), typeof(Double)], [GatewayIntents.None])]
public abstract partial class SentRequestHandler<THandler>;

[EventHandler([typeof(SocketSlashCommand)], [GatewayIntents.None])]
public abstract partial class SlashCommandExecutedHandler<THandler>;

[EventHandler([typeof(SocketStageChannel), typeof(SocketGuildUser)], [GatewayIntents.None])]
public abstract partial class SpeakerAddedHandler<THandler>;

[EventHandler([typeof(SocketStageChannel), typeof(SocketGuildUser)], [GatewayIntents.None])]
public abstract partial class SpeakerRemovedHandler<THandler>;

[EventHandler([typeof(SocketStageChannel)], [GatewayIntents.None])]
public abstract partial class StageEndedHandler<THandler>;

[EventHandler([typeof(SocketStageChannel)], [GatewayIntents.None])]
public abstract partial class StageStartedHandler<THandler>;

[EventHandler([typeof(SocketStageChannel), typeof(SocketStageChannel)], [GatewayIntents.None])]
public abstract partial class StageUpdatedHandler<THandler>;

[EventHandler([typeof(SocketSubscription)], [GatewayIntents.None])]
public abstract partial class SubscriptionCreatedHandler<THandler>;

[EventHandler([typeof(Cacheable<SocketSubscription, UInt64>)], [GatewayIntents.None])]
public abstract partial class SubscriptionDeletedHandler<THandler>;

[EventHandler([typeof(Cacheable<SocketSubscription, UInt64>), typeof(SocketSubscription)], [GatewayIntents.None])]
public abstract partial class SubscriptionUpdatedHandler<THandler>;

[EventHandler([typeof(SocketThreadChannel)], [GatewayIntents.None])]
public abstract partial class ThreadCreatedHandler<THandler>;

[EventHandler([typeof(Cacheable<SocketThreadChannel, UInt64>)], [GatewayIntents.None])]
public abstract partial class ThreadDeletedHandler<THandler>;

[EventHandler([typeof(SocketThreadUser)], [GatewayIntents.None])]
public abstract partial class ThreadMemberJoinedHandler<THandler>;

[EventHandler([typeof(SocketThreadUser)], [GatewayIntents.None])]
public abstract partial class ThreadMemberLeftHandler<THandler>;

[EventHandler([typeof(Cacheable<SocketThreadChannel, UInt64>), typeof(SocketThreadChannel)], [GatewayIntents.None])]
public abstract partial class ThreadUpdatedHandler<THandler>;

[EventHandler([typeof(String), typeof(JToken)], [GatewayIntents.None])]
public abstract partial class UnknownDispatchReceivedHandler<THandler>;

[EventHandler([typeof(SocketUser), typeof(SocketGuild)], [GatewayIntents.None])]
public abstract partial class UserBannedHandler<THandler>;

[EventHandler([typeof(SocketUserCommand)], [GatewayIntents.None])]
public abstract partial class UserCommandExecutedHandler<THandler>;

[EventHandler([typeof(Cacheable<IUser, UInt64>), typeof(Cacheable<IMessageChannel, UInt64>)], [GatewayIntents.None])]
public abstract partial class UserIsTypingHandler<THandler>;

[EventHandler([typeof(SocketGuildUser)], [GatewayIntents.None])]
public abstract partial class UserJoinedHandler<THandler>;

[EventHandler([typeof(SocketGuild), typeof(SocketUser)], [GatewayIntents.None])]
public abstract partial class UserLeftHandler<THandler>;

[EventHandler([typeof(SocketUser), typeof(SocketGuild)], [GatewayIntents.None])]
public abstract partial class UserUnbannedHandler<THandler>;

[EventHandler([typeof(SocketUser), typeof(SocketUser)], [GatewayIntents.None])]
public abstract partial class UserUpdatedHandler<THandler>;

[EventHandler([typeof(SocketUser), typeof(SocketVoiceState), typeof(SocketVoiceState)], [GatewayIntents.None])]
public abstract partial class UserVoiceStateUpdatedHandler<THandler>;

[EventHandler([typeof(Cacheable<SocketVoiceChannel, UInt64>), typeof(String), typeof(String)], [GatewayIntents.None])]
public abstract partial class VoiceChannelStatusUpdatedHandler<THandler>;

[EventHandler([typeof(SocketVoiceServer)], [GatewayIntents.None])]
public abstract partial class VoiceServerUpdatedHandler<THandler>;

[EventHandler([typeof(SocketGuild), typeof(SocketChannel)], [GatewayIntents.None])]
public abstract partial class WebhooksUpdatedHandler<THandler>;