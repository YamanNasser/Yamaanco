using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Yamaanco.Application.Common.Options;
using Yamaanco.Application.DTOs.Comment;
using Yamaanco.Application.DTOs.SystemNotifications;
using Yamaanco.Application.Features.GroupComments.Notifications;
using Yamaanco.Application.Interfaces;
using Yamaanco.Application.Interfaces.Repositories.Notifications;
using Yamaanco.Domain.Entities.GroupEntities;
using Yamaanco.Domain.Enums;

namespace Yamaanco.Application.Features.GroupComments.Handlers.Notifications
{
    public class CommentCreatedHandler : INotificationHandler<CommentCreated>
    {
        private readonly INotificationService _notification;
        private readonly IUnitOfWork _unitOfWork;
        private readonly INotificationsRepository _saredNotificationsCollection;

        public CommentCreatedHandler(INotificationService notification, IUnitOfWork unitOfWork,
           INotificationsRepository saredNotificationsCollection)
        {
            _saredNotificationsCollection = saredNotificationsCollection;
            _notification = notification;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(CommentCreated commentCreated, CancellationToken cancellationToken)
        {
            string message = "";

            var isPost = commentCreated.Comment.Root == null;

            PrepareNotificationMessageSubject(commentCreated, ref message);

            if (isPost)
            {
                await NotifyAllMembers(commentCreated, message); //Except creator and mentioned.
            }
            else //Case Reply
            {
                await NotifyAllPostParticipants(commentCreated, message); //Except creator and mentioned.
            }

            await _unitOfWork.CommitAsync();
        }

        private async Task NotifyAllPostParticipants(CommentCreated commentCreated, string message)
        {
            var msg = message;

            //Case there are mentioned member so the reply is related only for them.
            IEnumerable<string> participantsIdList = commentCreated.Comment
                .Pings
                .Select(o => o.UserId);

            if (participantsIdList.Count() == 0)
            { //Case there are no mentioned member, then notify all members who participant on the post.
                participantsIdList = _unitOfWork
          .GroupCommentTransactionRepository.Find(o => o.CommentRoot == commentCreated.Comment.Root && o.UserId != commentCreated.Comment.CreatedById)
          .Select(o => o.UserId);
            }
            else
            {
                msg = $"{commentCreated.Comment.CreatorName} mention you in some post on {commentCreated.Comment.CategoryName} group reply.";
            }

            if (participantsIdList != null && participantsIdList.Count() >= 1)
            {
                var participantUnSeenNotificationNumber = await _saredNotificationsCollection
              .GetNumberOfUnSeenGeneralNotificationForProfileList(participantsIdList.ToArray());

                foreach (var participantId in participantsIdList)
                {
                    AddNewNotification(commentCreated, msg, participantId);

                    await _notification.SendAsync(
                        new NewCommentNotificationMessage<CommentDto>()
                        {
                            From = commentCreated.Comment.CreatedById,
                            To = participantId,
                            Subject = msg,
                            NumberOfNotification = participantUnSeenNotificationNumber == null ? 1 : participantUnSeenNotificationNumber.GetValueOrDefault(participantId) + 1,
                            Body = commentCreated.Comment.Content,
                            Comment = commentCreated.Comment
                        });
                }
            }
        }

        private async Task NotifyAllMembers(CommentCreated commentCreated, string message)
        {
            var msg = message;

            var numberOfMemberNotification = await _saredNotificationsCollection
               .GetNumberOfUnSeenGeneralNotificationForGroupMembers(commentCreated.Comment.CategoryId);

            var groupMembersIdList = _unitOfWork.GroupMemberRepository
                .Find(o => o.GroupId == commentCreated.Comment.CategoryId && o.MemberId != commentCreated.Comment.CreatedById)
                .Select(o => o.Id);

            if (groupMembersIdList != null && groupMembersIdList.Any())
            {
                foreach (var member in groupMembersIdList)
                {
                    //Case: when member is mentioned in the comments.
                    if (commentCreated.Comment.Pings.Any(o => o.UserId == member))
                    {
                        msg = $"{commentCreated.Comment.CreatorName} mention you on {commentCreated.Comment.CategoryName} group post.";
                    }

                    AddNewNotification(commentCreated, msg, member);

                    await _notification.SendAsync(
                     new NewCommentNotificationMessage<CommentDto>()
                     {
                         From = commentCreated.Comment.CreatedById,
                         To = member,
                         Subject = msg,
                         NumberOfNotification =
                         numberOfMemberNotification == null ? 1 :
                         numberOfMemberNotification.GetValueOrDefault(member) + 1,
                         Body = commentCreated.Comment.Content,
                         Comment = commentCreated.Comment
                     });
                }
            }
        }

        private void AddNewNotification(CommentCreated commentCreated, string subject, string to)
        {
            _unitOfWork.GroupNotificationRepository.Add(
                new GroupNotification(
                sourceId: commentCreated.Comment.Id,
                notificationCategory: NotificationCategory.Group,
                content: commentCreated.Comment.Content,
                notificationType: commentCreated.Comment.Root == null ? NotificationType.NewComment : NotificationType.NewReply,
                participantId: commentCreated.Comment.CreatedById,
                profileId: to,
                groupId: commentCreated.Comment.CategoryId,
                    title: subject
                ));
        }

        private static void PrepareNotificationMessageSubject(CommentCreated commentCreated,
            ref string message)
        {
            var isPost = commentCreated.Comment.Root == null;
            var creatorName = commentCreated.Comment.CreatorName;
            var categoryName = commentCreated.Comment.CategoryName;

            if (isPost)
            {
                message = $"{creatorName} posted on {categoryName} group.";
            }
            else // Reply Case
            {
                message = $"{creatorName} commented on {categoryName} group post.";
            }
        }
    }
}