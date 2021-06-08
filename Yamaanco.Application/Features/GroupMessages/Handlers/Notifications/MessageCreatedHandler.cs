using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Yamaanco.Application.DTOs.Message;
using Yamaanco.Application.DTOs.SystemNotifications;
using Yamaanco.Application.Features.GroupMessages.Notifications;
using Yamaanco.Application.Interfaces;
using Yamaanco.Application.Interfaces.Repositories.Notifications;
using Yamaanco.Domain.Entities.GroupEntities;
using Yamaanco.Domain.Enums;

namespace Yamaanco.Application.Features.GroupMessages.Handlers.Notifications
{
    public class MessageCreatedHandler : INotificationHandler<MessageCreated>
    {
        private readonly INotificationService _notification;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTime _dateTime;
        private readonly INotificationsRepository _saredNotificationsCollection;

        public MessageCreatedHandler(INotificationService notification,
             INotificationsRepository saredNotificationsCollection,
            IUnitOfWork unitOfWork,
            IDateTime dateTime)
        {
            _saredNotificationsCollection = saredNotificationsCollection;
            _notification = notification;
            _unitOfWork = unitOfWork;
            _dateTime = dateTime;
        }

        public async Task Handle(MessageCreated messageCreated, CancellationToken cancellationToken)
        {
            string notificationMessage = $"{messageCreated.Message.ParticipantName} send a new message.";

            var groupMemberroupNotificationValues = await _saredNotificationsCollection
             .GetNumberOfUnSeenGeneralNotificationForGroupMembers(messageCreated.Message.CategoryId);

            foreach (var memberNotification in groupMemberroupNotificationValues)
            {
                _unitOfWork.GroupNotificationRepository
                 .Add(new GroupNotification(
                       sourceId: messageCreated.Message.Id,
                       notificationCategory: NotificationCategory.Group,
                       content: messageCreated.Message.Content,
                       notificationType: NotificationType.NewMessage,
                       participantId: messageCreated.Message.ParticipantId,
                       groupId: messageCreated.Message.CategoryId,
                       title: notificationMessage,
                       profileId: memberNotification.Key
                   ));

                await _notification.SendAsync(
                   new NewMessageNotificationMessage<MessageDto>()
                   {
                       From = messageCreated.Message.ParticipantId,
                       To = memberNotification.Key,
                       Subject = notificationMessage,
                       NumberOfNotification = memberNotification.Value + 1,
                       Body = messageCreated.Message.Content,
                       Message = messageCreated.Message
                   });
            }

            await _unitOfWork.CommitAsync();
        }
    }
}