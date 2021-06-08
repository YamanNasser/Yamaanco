using System.Threading.Tasks;
using Yamaanco.Application.Common.Options;
using Yamaanco.Application.DTOs.SystemNotifications;

namespace Yamaanco.Application.Interfaces
{
    public interface INotificationService
    {
        Task SendAsync<T>(NewCommentNotificationMessage<T> comment);

        Task SendAsync<T>(NewMessageNotificationMessage<T> message);

        Task SendAsync(NewMessageNotification message);

        Task SendAsync(NewFollowerMessage message);

        Task SendAsync(MultipleEmails message, EmailOptions emailSettings);

        Task SendAsync(SingleEmail message, EmailOptions emailSettings);
    }
}