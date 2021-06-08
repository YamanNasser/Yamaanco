namespace Yamaanco.Application.DTOs.SystemNotifications
{
    public class NewFollowerMessage : Message
    {
        public int NumberOfUnSeenFollowers { get; set; }
    }
}