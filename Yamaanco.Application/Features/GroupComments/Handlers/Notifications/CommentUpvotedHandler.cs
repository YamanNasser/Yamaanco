using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Yamaanco.Application.DTOs.SystemNotifications;
using Yamaanco.Application.Features.GroupComments.Notifications;
using Yamaanco.Application.Interfaces;
using Yamaanco.Application.Interfaces.Repositories.Notifications;
using Yamaanco.Domain.Entities.GroupEntities;
using Yamaanco.Domain.Enums;

namespace Yamaanco.Application.Features.GroupComments.Handlers.Notifications
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
            var userGrouptMessage = $"{comment.UpvoteByName} upvoted your comment on Group {comment.GroupName}";

            await NotifyCommentOwner(comment, userGrouptMessage);
            await _unitOfWork.CommitAsync();
        }

        private async Task NotifyCommentOwner(CommentUpvoted comment, string userGrouptMessage)
        {
            var numberOfGroupNotification = await _saredNotificationsCollection
                .GetNumberOfUnSeenMessageNotification(comment.CreatedById);

            AddNewNotification(comment, userGrouptMessage, comment.CreatedById);

            await _notification.SendAsync(
               new NewMessageNotification()
               {
                   From = comment.UpvoteById,
                   To = comment.CreatedById,
                   Subject = userGrouptMessage,
                   NumberOfNotification = numberOfGroupNotification + 1,
                   Body = comment.Content
               });
        }

        private void AddNewNotification(CommentUpvoted comment, string subject, string to)
        {
            _unitOfWork.GroupNotificationRepository.Add(
          new GroupNotification(
          sourceId: comment.Id,
          notificationCategory: NotificationCategory.Group,
          content: comment.Content,
          notificationType: comment.Root == null ? NotificationType.UpvotedComment : NotificationType.UpvotedReply,
          title: subject,
          participantId: comment.CreatedById,
          profileId: to,
          groupId: comment.GroupId
          ));
        }
    }
}