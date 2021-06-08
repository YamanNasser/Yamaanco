using Yamaanco.Application.Structs;

namespace Yamaanco.Application.DTOs.SystemNotifications
{
    public class GeneralNotification
    {
        public string Photo { get; set; }
        public string Link { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsSeen { get; set; }
        public ElapsedTimeValue ElapsedTime { get; set; }
    }
}