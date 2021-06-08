namespace Yamaanco.Application.DTOs.SystemNotifications
{
    public class NewMessageNotificationMessage<T> : Message
    {
        public int NumberOfNotification { get; set; }
        public T Message { get; set; }
    }
}