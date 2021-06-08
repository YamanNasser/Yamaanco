using System;
using Yamaanco.Application.Structs;
using Yamaanco.Domain.Enums;

namespace Yamaanco.Application.Features.Notifications.ViewModel
{
    public class UnSeenNotificationsListView
    {
        public string CategoryId { get; set; }
        public string Target { get; set; }

        public string ParticipantPhotoPath { get; set; }

        public string CategoryName { get; set; }
        public NotificationCategory Category { get; set; }
        public NotificationType Type { get; set; }

        public DateTime CreatedDate { get; set; }

        public int GenderId { get; set; }

        public string Content { get; set; }

        public ElapsedTimeValue ElapsedTime { get; set; }
    }
}