using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Yamaanco.Application.Common.Options;
using Yamaanco.Application.DTOs.SystemNotifications;
using Yamaanco.Application.Features.ProfileComments.Notifications;
using Yamaanco.Application.Interfaces;
using Yamaanco.Application.Interfaces.Repositories.Notifications;
using Yamaanco.Domain.Entities.ProfileEntities;
using Yamaanco.Domain.Enums;

namespace Yamaanco.Application.Features.ProfileComments.Handlers.Notifications
{
    public class CommentUpdatedHandler : INotificationHandler<CommentUpdated>
    {
        private readonly INotificationService _notification;
        private readonly EmailOptions _emailSettings;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTime _dateTime;
        private readonly INotificationsRepository _saredNotificationsCollection;

        public CommentUpdatedHandler(INotificationService notification,
            IOptions<EmailOptions> emailSettings, IUnitOfWork unitOfWork,
           INotificationsRepository saredNotificationsCollection, IDateTime dateTime)
        {
            _saredNotificationsCollection = saredNotificationsCollection;
            _emailSettings = emailSettings.Value;
            _notification = notification;
            _unitOfWork = unitOfWork;
            _dateTime = dateTime;
        }

        public async Task Handle(CommentUpdated comment, CancellationToken cancellationToken)
        {
            string userProfiletMessage = "";
            string participantOrFollowersMessage = "";
            var isProfileOwnerWhoPostTheComment = comment.CategoryId == comment.UpdatedById;
            var isPost = comment.Root == null;
            //---------------------------------------------------------------------------
            PrepareNotificationMessageSubject(comment, ref userProfiletMessage, ref participantOrFollowersMessage);

            await AlwaysNotifyMentionedUsers(comment);
            //---------------------------------------------------------------------------

            if (isPost && !isProfileOwnerWhoPostTheComment)
            {
                await NotifyProfileOwner(comment, userProfiletMessage);
            }
            else //when Reply case then send notification to the post participant.
            {
                await NotifyParticipants(comment, participantOrFollowersMessage);

                if (!isProfileOwnerWhoPostTheComment)
                {
                    await NotifyProfileOwner(comment, userProfiletMessage);
                }
            }

            await _unitOfWork.CommitAsync();
        }

        private async Task NotifyParticipants(CommentUpdated comment, string participantOrFollowersMessage)
        {
            //Get Participants: any person who has update reply or make like. In addition, not mentioned
            var participants = _unitOfWork
                .ProfileCommentTransactionRepository
                .GetCommentParticipantsIdList(comment.Id, comment.CategoryId, comment.Pings);

            if (participants != null && participants.Any())
            {
                var participantUnSeenNotificationNumber = await _saredNotificationsCollection.GetNumberOfUnSeenGeneralNotificationForProfileFollower(comment.CategoryId);

                if (participantUnSeenNotificationNumber != null)
                    foreach (var participant in participants)
                    {
                        AddNewNotification(comment, participantOrFollowersMessage, participant);

                        await _notification.SendAsync(
                            new NewCommentNotificationMessage<CommentUpdated>()
                            {
                                From = comment.UpdatedById,
                                To = participant,
                                Subject = participantOrFollowersMessage,
                                NumberOfNotification = participantUnSeenNotificationNumber == null ? 1 : participantUnSeenNotificationNumber.GetValueOrDefault(participant) + 1,
                                Body = comment.Content,
                                Comment = comment
                            });
                    }
            }
        }

        private async Task NotifyProfileOwner(CommentUpdated comment, string userProfiletMessage)
        {
            // Notify the profile owner that someone post on his/her profile wall.

            var numberOfProfileNotification = await _saredNotificationsCollection
                   .GetNumberOfUnSeenMessageNotification(comment.CategoryId);

            AddNewNotification(comment, userProfiletMessage, comment.CategoryId);

            await _notification.SendAsync(
               new NewCommentNotificationMessage<CommentUpdated>()
               {
                   From = comment.UpdatedById,
                   To = comment.CategoryId,
                   Subject = userProfiletMessage,
                   NumberOfNotification = numberOfProfileNotification + 1,
                   Body = comment.Content,
                   Comment = comment
               });
        }

        private async Task AlwaysNotifyMentionedUsers(CommentUpdated comment)
        {
            var isProfileOwnerWhoPostTheComment = comment.CategoryId == comment.UpdatedById;
            var msg = !isProfileOwnerWhoPostTheComment ? $"{comment.UpdatedByName} mention you on {comment.CategoryName} profile." : $"{comment.UpdatedByName} mention you.";

            //always notify mentioned users, and exclude the profile owner. By default profile owner will notified when post/reply added profile wall.
            var numberOfMentionedNotification = await _saredNotificationsCollection
               .GetNumberOfUnSeenGeneralNotificationForProfileList(comment
               .Pings);
            if (numberOfMentionedNotification != null)
                foreach (var mentioned in comment.Pings.Where(o => o != comment.CategoryId))
                {
                    AddNewNotification(comment, msg, mentioned);

                    await _notification.SendAsync(
                     new NewCommentNotificationMessage<CommentUpdated>()
                     {
                         From = comment.UpdatedById,
                         To = mentioned,
                         Subject = msg,
                         NumberOfNotification = numberOfMentionedNotification == null ? 1 : numberOfMentionedNotification.GetValueOrDefault(mentioned) + 1,
                         Body = comment.Content,
                         Comment = comment
                     });
                }
        }

        private void AddNewNotification(CommentUpdated comment, string subject, string to)
        {
            _unitOfWork.ProfileNotificationRepository.Add(
           new ProfileNotification(
           sourceId: comment.Id,
           notificationCategory: NotificationCategory.Profile,
           content: comment.Content,
           notificationType: comment.Root == null ? NotificationType.UpdateComment : NotificationType.UpdateReply,
           participantId: to,
           profileId: comment.CategoryId,
           title: subject
           ));
        }

        private static void PrepareNotificationMessageSubject(CommentUpdated comment, ref string userProfiletMessage, ref string participantOrFollowersMessage)
        {
            var isProfileOwnerWhoPostTheComment = comment.CategoryId == comment.UpdatedById;
            var isPost = comment.Root == null;

            if (isPost)
            {
                if (isProfileOwnerWhoPostTheComment)
                {
                    participantOrFollowersMessage = $"Some post updated by {comment.UpdatedByName}.";
                }
                else
                {
                    participantOrFollowersMessage = $"Some post updated by  {comment.UpdatedByName}  on {comment.CategoryName} profile.";

                    userProfiletMessage = $"Some post updated by {comment.UpdatedByName} on your profile.";
                }
            }
            else // Reply Case
            {
                if (isProfileOwnerWhoPostTheComment)
                {
                    participantOrFollowersMessage = $"Some reply updated by {comment.UpdatedByName}.";
                }
                else
                {
                    participantOrFollowersMessage = $"Some reply updated by {comment.UpdatedByName} on {comment.CategoryName} profile.";

                    userProfiletMessage = $"Some reply updated by {comment.UpdatedByName} on your profile.";
                }
            }
        }
    }
}