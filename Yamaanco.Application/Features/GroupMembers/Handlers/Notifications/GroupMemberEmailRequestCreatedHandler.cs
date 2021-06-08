using MediatR;
using Microsoft.Extensions.Options;
using System.Threading;
using System.Threading.Tasks;
using Yamaanco.Application.Common.Options;
using Yamaanco.Application.DTOs.SystemNotifications;
using Yamaanco.Application.Features.GroupMembers.Notifications;
using Yamaanco.Application.Interfaces;

namespace Yamaanco.Application.Features.GroupMembers.Handlers.Notifications
{
    public class GroupMemberEmailRequestCreatedHandler : INotificationHandler<GroupMemberEmailRequestCreated>
    {
        private readonly INotificationService _notification;
        private readonly EmailOptions _emailSettings;

        public GroupMemberEmailRequestCreatedHandler(INotificationService notification,
            IOptions<EmailOptions> emailSettings)
        {
            _emailSettings = emailSettings.Value;
            _notification = notification;
        }

        public async Task Handle(GroupMemberEmailRequestCreated groupMemberRequest, CancellationToken cancellationToken)
        {
            await _notification.SendAsync(
                new SingleEmail()
                {
                    To = groupMemberRequest.InvitedEmail,
                    Subject = $"Join this Great Group! ",
                    Body = @$"<p> <b>Hello</b> <p>
                               <p> {groupMemberRequest.InviterName} invited you to join the following group name: {groupMemberRequest.GroupName}</p>
                           <p> To accept this invitation, click <a href='{groupMemberRequest.URL}'>Join this group</a>.</p>
                        <p> <b>Thank You</b> :) </p>
                        "
                }, _emailSettings);
        }
    }
}