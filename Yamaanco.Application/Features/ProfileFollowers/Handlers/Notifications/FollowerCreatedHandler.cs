using MediatR;
using Microsoft.Extensions.Options;
using System.Threading;
using System.Threading.Tasks;
using Yamaanco.Application.Common.Options;
using Yamaanco.Application.DTOs.SystemNotifications;
using Yamaanco.Application.Features.GroupFollowers.Notifications.Created;
using Yamaanco.Application.Interfaces;

namespace Yamaanco.Application.Features.GroupFollowers.Handlers.Notifications
{
    public class FollowerCreatedHandler : INotificationHandler<FollowerCreated>
    {
        private readonly INotificationService _notification;
        private readonly EmailOptions _emailSettings;

        public FollowerCreatedHandler(INotificationService notification, IOptions<EmailOptions> emailSettings)
        {
            _emailSettings = emailSettings.Value;
            _notification = notification;
        }

        public async Task Handle(FollowerCreated notification, CancellationToken cancellationToken)
        {
            await _notification.SendAsync(
                new NewFollowerMessage()
                {
                    From = notification.FollowerId,
                    To = notification.ProfileId,
                    NumberOfUnSeenFollowers = notification.NumberOfUnSeenProfileFollowers,
                    Subject = $"{notification.FollowerName} Follows you",
                    Body = @$"<h1>({notification.FollowerName}) is now following your profile({notification.ProfileUserName}) <h1>
                        "
                });

            await _notification.SendAsync(
                new SingleEmail()
                {
                    To = notification.ProfileEmail,
                    Subject = $"{notification.FollowerName} Follows you",
                    Body = @$"<h1>({notification.FollowerName}) is now following your profile({notification.ProfileUserName})  <h1>
                        "
                }, _emailSettings);
        }
    }
}