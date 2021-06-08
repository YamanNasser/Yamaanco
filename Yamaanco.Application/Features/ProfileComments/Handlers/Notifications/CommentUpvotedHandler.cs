using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Yamaanco.Application.DTOs.SystemNotifications;
using Yamaanco.Application.Features.ProfileComments.Notifications;
using Yamaanco.Application.Interfaces;
using Yamaanco.Application.Interfaces.Repositories.Notifications;
using Yamaanco.Domain.Entities.ProfileEntities;
using Yamaanco.Domain.Enums;

namespace Yamaanco.Application.Features.ProfileComments.Handlers.Notifications
{
    public class CommentUpvotedHandler : INotificationHandler<CommentUpvoted>
    {
        private readonly INotificationService _notification;
        private readonly IUnitOfWork _unitOfWork;
        private readonly INotificationsRepository _saredNotificationsCollection;

        public CommentUpvotedHandler(INotificationService notification,
            IUnitOfWork unitOfWork,
           INotificationsRepository saredNotificationsCollection)
        {
            _saredNotificationsCollection = saredNotificationsCollection;
            _notification = notification;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(CommentUpvoted comment, CancellationToken cancellationToken)
        {
            var isProfileOwnerWhoPostTheComment = comment.ProfileId == comment.UpvoteById;

            if (!isProfileOwnerWhoPostTheComment)
            {
                var userProfiletMessage = $"{comment.UpvoteByName} upvoted comment on your profile ";

                await NotifyProfileOwner(comment, userProfiletMessage);
                await _unitOfWork.CommitAsync();
            }
        }

        private async Task NotifyProfileOwner(CommentUpvoted comment, string userProfiletMessage)
        {
            // Notify the profile owner that someone upvoted  post on his/her profile wall.

            var numberOfProfileNotification = await _saredNotificationsCollection
                .GetNumberOfUnSeenMessageNotification(comment.ProfileId);

            AddNewNotification(comment, userProfiletMessage, comment.ProfileId);

            await _notification.SendAsync(
               new NewCommentNotificationMessage<CommentUpvoted>()
               {
                   From = comment.UpvoteById,
                   To = comment.ProfileId,
                   Subject = userProfiletMessage,
                   NumberOfNotification = numberOfProfileNotification + 1,
                   Body = comment.Content,
                   Comment = comment
               });
        }

        private void AddNewNotification(CommentUpvoted comment, string subject, string to)
        {
            _unitOfWork.ProfileNotificationRepository.Add(
          new ProfileNotification(
          sourceId: comment.Id,
          notificationCategory: NotificationCategory.Profile,
          content: comment.Content,
          notificationType: comment.Root == null ? NotificationType.UpvotedComment : NotificationType.UpvotedReply,
          participantId: to,
          profileId: comment.ProfileId,
          title: subject
          ));
        }
    }
}