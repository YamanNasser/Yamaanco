using System.Threading.Tasks;
using Yamaanco.Application.Common.Options;
using Yamaanco.Application.DTOs.SystemNotifications;
using Yamaanco.Application.Interfaces;
using Yamaanco.Infrastructure.Shared.Emails;

namespace Yamaanco.Infrastructure.Shared.Notifications
{
    public class NotificationService : INotificationService
    {
        public async Task SendAsync(MultipleEmails message, EmailOptions emailSettings)
        {
            EmailSenderService email = new EmailSenderService(emailSettings);
            await email.SendEmailAsync(message.To, message.Subject, message.Body);
        }

        public async Task SendAsync(SingleEmail message, EmailOptions emailSettings)
        {
            EmailSenderService email = new EmailSenderService(emailSettings);
            await email.SendEmailAsync(message.To, message.Subject, message.Body);
        }

        public async Task SendAsync<T>(NewCommentNotificationMessage<T> message)
        {
            await Task.CompletedTask;
        }

        public async Task SendAsync(NewMessageNotification message)
        {
            await Task.CompletedTask;
        }

        public async Task SendAsync(NewFollowerMessage message)
        {
            await Task.CompletedTask;
        }

        public async Task SendAsync<T>(NewMessageNotificationMessage<T> message)
        {
            await Task.CompletedTask;
        }
    }
}