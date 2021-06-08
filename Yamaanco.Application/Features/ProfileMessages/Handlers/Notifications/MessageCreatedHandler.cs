using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Yamaanco.Application.DTOs.Message;
using Yamaanco.Application.DTOs.SystemNotifications;
using Yamaanco.Application.Features.ProfileMessages.Notifications;
using Yamaanco.Application.Interfaces;
using Yamaanco.Application.Interfaces.Repositories.Notifications;
using Yamaanco.Domain.Entities.ProfileEntities;
using Yamaanco.Domain.Enums;

namespace Yamaanco.Application.Features.ProfileMessages.Handlers.Notifications
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

            var isProfileOwnerWhoAddTheMessage = messageCreated.Message.CategoryId == messageCreated.Message.ParticipantId;

            if (!isProfileOwnerWhoAddTheMessage)
            {
                _unitOfWork.ProfileNotificationRepository
                   .Add(new ProfileNotification(
                       sourceId: messageCreated.Message.Id,
                       notificationCategory: NotificationCategory.Profile,
                       content: messageCreated.Message.Content,
                       notificationType: NotificationType.NewMessage,
                       participantId: messageCreated.Message.ParticipantId,
                       profileId: messageCreated.Message.CategoryId,
                       title: notificationMessage
                   ));

                var numberOfProfileNotification = await _saredNotificationsCollection
                 .GetNumberOfUnSeenMessageNotification(messageCreated.Message.CategoryId);

                await _notification.SendAsync(
                       new NewMessageNotificationMessage<MessageDto>()
                       {
                           From = messageCreated.Message.ParticipantId,
                           To = messageCreated.Message.CategoryId,
                           Subject = notificationMessage,
                           NumberOfNotification = numberOfProfileNotification + 1,
                           Body = messageCreated.Message.Content,
                           Message = messageCreated.Message
                       });
            }

            await _unitOfWork.CommitAsync();
        }
    }
}