using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Yamaanco.Application.Common.Options;
using Yamaanco.Application.DTOs.SystemNotifications;
using Yamaanco.Application.Features.GroupComments.Notifications;
using Yamaanco.Application.Interfaces;
using Yamaanco.Application.Interfaces.Repositories.Notifications;
using Yamaanco.Domain.Entities.GroupEntities;
using Yamaanco.Domain.Enums;

namespace Yamaanco.Application.Features.GroupComments.Handlers.Notifications
{
    public class CommentUpdatedHandler : INotificationHandler<CommentUpdated>
    {
        private readonly INotificationService _notification;
        private readonly IUnitOfWork _unitOfWork;
        private readonly INotificationsRepository _saredNotificationsCollection;

        public CommentUpdatedHandler(INotificationService notification, IUnitOfWork unitOfWork,
           INotificationsRepository saredNotificationsCollection)
        {
            _saredNotificationsCollection = saredNotificationsCollection;
            _notification = notification;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(CommentUpdated CommentUpdated, CancellationToken cancellationToken)
        {
            string message = "";

            var isPost = CommentUpdated.Root == null;

            PrepareNotificationMessageSubject(CommentUpdated, ref message);

            if (isPost)
            {
                await NotifyAllMembers(CommentUpdated, message); //Except creator and mentioned.
            }
            else //Case Reply
            {
                await NotifyAllPostParticipants(CommentUpdated, message); //Except creator and mentioned.
            }

            await _unitOfWork.CommitAsync();
        }

        private async Task NotifyAllPostParticipants(CommentUpdated CommentUpdated, string message)
        {
            var msg = message;

            //Case there are mentioned member so the reply is related only for them.
            IEnumerable<string> participantsIdList = CommentUpdated
                .Pings;

            if (participantsIdList.Count() == 0)
            { //Case there are no mentioned member, then notify all members who participant on the post.
                participantsIdList = _unitOfWork
          .GroupCommentTransactionRepository.Find(o => o.CommentRoot == CommentUpdated.Root && o.UserId != CommentUpdated.UpdatedById)
          .Select(o => o.UserId);
            }
            else
            {
                msg = $"{CommentUpdated.UpdatedByName} mention you in some post on {CommentUpdated.CategoryName} group reply.";
            }

            if (participantsIdList != null && participantsIdList.Count() >= 1)
            {
                var participantUnSeenNotificationNumber = await _saredNotificationsCollection
              .GetNumberOfUnSeenGeneralNotificationForProfileList(participantsIdList.ToArray());

                foreach (var participantId in participantsIdList)
                {
                    AddNewNotification(CommentUpdated, msg, participantId);

                    await _notification.SendAsync(
                        new NewCommentNotificationMessage<CommentUpdated>()
                        {
                            From = CommentUpdated.UpdatedById,
                            To = participantId,
                            Subject = msg,
                            NumberOfNotification = participantUnSeenNotificationNumber == null ? 1 : participantUnSeenNotificationNumber.GetValueOrDefault(participantId) + 1,
                            Body = CommentUpdated.Content,
                            Comment = CommentUpdated
                        });
                }
            }
        }

        private async Task NotifyAllMembers(CommentUpdated CommentUpdated, string message)
        {
            var msg = message;

            var numberOfMemberNotification = await _saredNotificationsCollection
               .GetNumberOfUnSeenGeneralNotificationForGroupMembers(CommentUpdated.CategoryId);

            var groupMembersIdList = _unitOfWork.GroupMemberRepository
                .Find(o => o.GroupId == CommentUpdated.CategoryId
                && o.MemberId != CommentUpdated.UpdatedById)
                .Select(o => o.Id);

            if (groupMembersIdList != null && groupMembersIdList.Any())
            {
                foreach (var member in groupMembersIdList)
                {
                    //Case: when member is mentioned in the comments.
                    if (CommentUpdated.Pings.Any())
                    {
                        msg = $"{CommentUpdated.UpdatedById} mention you on {CommentUpdated.CategoryName} group post.";
                    }

                    AddNewNotification(CommentUpdated, msg, member);

                    await _notification.SendAsync(
                     new NewCommentNotificationMessage<CommentUpdated>()
                     {
                         From = CommentUpdated.UpdatedById,
                         To = member,
                         Subject = msg,
                         NumberOfNotification =
                         numberOfMemberNotification == null ? 1 :
                         numberOfMemberNotification.GetValueOrDefault(member) + 1,
                         Body = CommentUpdated.Content,
                         Comment = CommentUpdated
                     });
                }
            }
        }

        private void AddNewNotification(CommentUpdated comment, string subject, string to)
        {
            _unitOfWork.GroupNotificationRepository.Add(
          new GroupNotification(
          sourceId: comment.Id,
          notificationCategory: NotificationCategory.Group,
          content: comment.Content,
          notificationType: comment.Root == null ? NotificationType.UpdateComment : NotificationType.UpdateReply,
          participantId: comment.UpdatedById,
          profileId: to,
          groupId: comment.CategoryId,
          title: subject
          ));
        }

        private static void PrepareNotificationMessageSubject(CommentUpdated CommentUpdated,
            ref string message)
        {
            var isPost = CommentUpdated.Root == null;
            var updatedByName = CommentUpdated.UpdatedByName;
            var categoryName = CommentUpdated.CategoryName;

            if (isPost)
            {
                message = $"{updatedByName} updated post on {categoryName} group.";
            }
            else // Reply Case
            {
                message = $"{updatedByName} updated comment on {categoryName} group post.";
            }
        }
    }
}