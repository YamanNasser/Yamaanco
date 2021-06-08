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
using Yamaanco.Application.Features.ProfileComments.Notifications;
using Yamaanco.Application.Interfaces;
using Yamaanco.Application.Interfaces.Repositories.Notifications;
using Yamaanco.Domain.Entities.ProfileEntities;
using Yamaanco.Domain.Enums;

namespace Yamaanco.Application.Features.ProfileComments.Handlers.Notifications
{
    public class CommentCreatedHandler : INotificationHandler<CommentCreated>
    {
        private readonly INotificationService _notification;
        private readonly EmailOptions _emailSettings;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTime _dateTime;
        private readonly INotificationsRepository _saredNotificationsCollection;

        public CommentCreatedHandler(INotificationService notification,
            IOptions<EmailOptions> emailSettings, IUnitOfWork unitOfWork,
           INotificationsRepository saredNotificationsCollection, IDateTime dateTime)
        {
            _saredNotificationsCollection = saredNotificationsCollection;
            _emailSettings = emailSettings.Value;
            _notification = notification;
            _unitOfWork = unitOfWork;
            _dateTime = dateTime;
        }

        public async Task Handle(CommentCreated commentCreated, CancellationToken cancellationToken)
        {
            string userProfiletMessage = "";
            string participantOrFollowersMessage = "";
            var isProfileOwnerWhoPostTheComment = commentCreated.Comment.CategoryId == commentCreated.Comment.CreatedById;
            var isPost = commentCreated.Comment.Root == null;
            //---------------------------------------------------------------------------
            PrepareNotificationMessageSubject(commentCreated, ref userProfiletMessage, ref participantOrFollowersMessage);

            await AlwaysNotifyMentionedUsers(commentCreated);
            //---------------------------------------------------------------------------

            if (isPost)
            {
                await NotifyFollowers(commentCreated, participantOrFollowersMessage);

                if (!isProfileOwnerWhoPostTheComment)
                {
                    await NotifyProfileOwner(commentCreated, userProfiletMessage);
                }
            }
            else //when Reply case then send notification to the post participant.
            {
                await NotifyParticipants(commentCreated, participantOrFollowersMessage);

                if (!isProfileOwnerWhoPostTheComment)
                {
                    await NotifyProfileOwner(commentCreated, userProfiletMessage);
                }
            }
            await _unitOfWork.CommitAsync();
        }

        private async Task NotifyParticipants(CommentCreated commentCreated, string participantOrFollowersMessage)
        {
            //Get Participants: any person who has add reply or make like. In addition, not mentioned
            var participants = _unitOfWork
             .ProfileCommentTransactionRepository
             .GetCommentParticipantsIdList(commentCreated.Comment.Id, commentCreated.Comment.CategoryId,
             commentCreated.Comment.Pings.Select(o => o.UserId)?.ToArray());

            var participantUnSeenNotificationNumber = await _saredNotificationsCollection
                                               .GetNumberOfUnSeenGeneralNotificationForProfileFollower(commentCreated.Comment.CategoryId);

            if (participants != null && participants.Count() >= 1)
            {
                foreach (var participant in participants)
                {
                    AddNewNotification(commentCreated, participantOrFollowersMessage, participant);

                    await _notification.SendAsync(
                        new NewCommentNotificationMessage<CommentDto>()
                        {
                            From = commentCreated.Comment.CreatedById,
                            To = participant,
                            Subject = participantOrFollowersMessage,
                            NumberOfNotification = participantUnSeenNotificationNumber == null ? 1 : participantUnSeenNotificationNumber.GetValueOrDefault(participant) + 1,
                            Body = commentCreated.Comment.Content,
                            Comment = commentCreated.Comment
                        });
                }
            }
        }

        private async Task NotifyProfileOwner(CommentCreated commentCreated, string userProfiletMessage)
        {
            // Notify the profile owner that someone post on his/her profile wall.

            var numberOfProfileNotification = await _saredNotificationsCollection
                   .GetNumberOfUnSeenMessageNotification(commentCreated.Comment.CategoryId);

            AddNewNotification(commentCreated, userProfiletMessage, commentCreated.Comment.CategoryId);

            await _notification.SendAsync(
               new NewCommentNotificationMessage<CommentDto>()
               {
                   From = commentCreated.Comment.CreatedById,
                   To = commentCreated.Comment.CategoryId,
                   Subject = userProfiletMessage,
                   NumberOfNotification = numberOfProfileNotification + 1,
                   Body = commentCreated.Comment.Content,
                   Comment = commentCreated.Comment
               });
        }

        private async Task NotifyFollowers(CommentCreated commentCreated, string participantOrFollowersMessage)
        {
            var numberOfFollowerNotification = await _saredNotificationsCollection
                                                .GetNumberOfUnSeenGeneralNotificationForProfileFollower(commentCreated.Comment.CategoryId);

            var unMentionedFollowersList = await _unitOfWork.ProfileFollowerRepository.GetUnMentiondFollowersIdList(commentCreated.Comment.Pings.Select(o => o.UserId).ToList(), commentCreated.Comment.CategoryId);

            if (unMentionedFollowersList != null && unMentionedFollowersList.Count() >= 1)
            {
                //Notify profile followers, who not mentioned.
                foreach (var follower in unMentionedFollowersList)
                {
                    AddNewNotification(commentCreated, participantOrFollowersMessage, follower);

                    await _notification.SendAsync(
                     new NewCommentNotificationMessage<CommentDto>()
                     {
                         From = commentCreated.Comment.CreatedById,
                         To = follower,
                         Subject = participantOrFollowersMessage,
                         NumberOfNotification =
                         numberOfFollowerNotification == null ? 1 :
                         numberOfFollowerNotification.GetValueOrDefault(follower) + 1,
                         Body = commentCreated.Comment.Content,
                         Comment = commentCreated.Comment
                     });
                }
            }
        }

        private async Task AlwaysNotifyMentionedUsers(CommentCreated commentCreated)
        {
            var isProfileOwnerWhoPostTheComment = commentCreated.Comment.CategoryId == commentCreated.Comment.CreatedById;

            var msg = !isProfileOwnerWhoPostTheComment ?
                $"{commentCreated.Comment.CreatorName} mention you on {commentCreated.Comment.CategoryName} profile." :
                $"{commentCreated.Comment.CreatorName} mention you.";

            var numberOfMentionedNotification = await _saredNotificationsCollection
                .GetNumberOfUnSeenGeneralNotificationForProfileList(
                commentCreated.Comment.Pings.Select(o => o.UserId)?.ToArray());

            if (commentCreated.Comment.Pings != null && commentCreated.Comment.Pings.Count() >= 1)
            {
                //always notify mentioned users, and exclude the profile owner. By default profile owner will notified when post/reply added profile wall.
                foreach (var mentioned in commentCreated.Comment.Pings.Where(o => o.UserId != commentCreated.Comment.CategoryId))
                {
                    AddNewNotification(commentCreated, msg, mentioned.UserId);

                    await _notification.SendAsync(
                     new NewCommentNotificationMessage<CommentDto>()
                     {
                         From = commentCreated.Comment.CreatedById,
                         To = mentioned.UserId,
                         Subject = msg,
                         NumberOfNotification = numberOfMentionedNotification == null ? 1 : numberOfMentionedNotification.GetValueOrDefault(mentioned.UserId) + 1,
                         Body = commentCreated.Comment.Content,
                         Comment = commentCreated.Comment
                     });
                }
            }
        }

        private void AddNewNotification(CommentCreated commentCreated, string subject, string to)
        {
            _unitOfWork.ProfileNotificationRepository.Add(
                new ProfileNotification(sourceId: commentCreated.Comment.Id,
                notificationCategory: NotificationCategory.Profile,
                content: commentCreated.Comment.Content,
                notificationType: commentCreated.Comment.Root == null ? NotificationType.NewComment : NotificationType.NewReply,
                participantId: to,
                profileId: commentCreated.Comment.CategoryId,
                    title: subject
                ));
        }

        private static void PrepareNotificationMessageSubject(CommentCreated commentCreated, ref string userProfiletMessage, ref string participantOrFollowersMessage)
        {
            var isProfileOwnerWhoPostTheComment = commentCreated.Comment.CategoryId == commentCreated.Comment.CreatedById;
            var isPost = commentCreated.Comment.Root == null;

            var creatorName = commentCreated.Comment.CreatorName;
            var categoryName = commentCreated.Comment.CategoryName;
            if (isPost)
            {
                if (isProfileOwnerWhoPostTheComment)
                {
                    participantOrFollowersMessage = $"{creatorName} add a new post.";
                }
                else
                {
                    participantOrFollowersMessage = $"{creatorName} posted on {categoryName} profile.";

                    userProfiletMessage = $"{creatorName} posted on your profile.";
                }
            }
            else // Reply Case
            {
                if (isProfileOwnerWhoPostTheComment)
                {
                    participantOrFollowersMessage = $"{creatorName} add a new reply.";
                }
                else
                {
                    participantOrFollowersMessage = $"{creatorName} commented on {categoryName} profile post.";

                    userProfiletMessage = $"{creatorName} commented on your profile post.";
                }
            }
        }
    }
}