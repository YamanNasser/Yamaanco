using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Yamaanco.Application.Common.Options;
using Yamaanco.Application.DTOs.SystemNotifications;
using Yamaanco.Application.Features.GroupMembers.Notifications;
using Yamaanco.Application.Interfaces;
using Yamaanco.Application.Interfaces.Repositories.Notifications;
using Yamaanco.Domain.Entities.GroupEntities;
using Yamaanco.Domain.Enums;

namespace Yamaanco.Application.Features.GroupMembers.Handlers.Notifications
{
    public class NewMemberAddedHandler : INotificationHandler<NewMemberAdded>
    {
        private readonly INotificationService _notification;
        private readonly EmailOptions _emailSettings;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTime _dateTime;
        private readonly INotificationsRepository _saredNotificationsCollection;

        public NewMemberAddedHandler(INotificationService notification,
            IOptions<EmailOptions> emailSettings, IUnitOfWork unitOfWork,
           INotificationsRepository saredNotificationsCollection, IDateTime dateTime)
        {
            _saredNotificationsCollection = saredNotificationsCollection;
            _emailSettings = emailSettings.Value;
            _notification = notification;
            _unitOfWork = unitOfWork;
            _dateTime = dateTime;
        }

        public async Task Handle(NewMemberAdded addedMember, CancellationToken cancellationToken)
        {
            _unitOfWork.GroupNotificationRepository.Add(
               new GroupNotification(
                   sourceId: addedMember.GroupId,
                   notificationCategory: NotificationCategory.Group,
                   content: $"{addedMember.AddedByName} added you on Group {addedMember.GroupName}.",
                   notificationType: NotificationType.GroupRequest,
                   participantId: addedMember.AddedById,
                   groupId: addedMember.GroupId,
                   title: $"You become member of {addedMember.GroupName} group",
                   profileId: addedMember.MemberId
               ));

            await _unitOfWork.CommitAsync();

            await _notification.SendAsync(
                new SingleEmail()
                {
                    To = addedMember.Email,
                    Subject = $"You become member of {addedMember.GroupName} group",
                    Body = @$"<p> <b>Hello</b> <p>
                               <p> {addedMember.AddedByName} added you to the group name: {addedMember.GroupName}</p>
                        <p> <b>Thank You</b> :) </p>
                        "
                }, _emailSettings);
        }
    }
}