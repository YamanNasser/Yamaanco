namespace Yamaanco.Application.DTOs.SystemNotifications
{
    public class NewCommentNotificationMessage<T> : Message
    {
        public int NumberOfNotification { get; set; }
        public T Comment { get; set; }
    }
}