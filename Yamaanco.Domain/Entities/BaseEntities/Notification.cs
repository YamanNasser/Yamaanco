using System;
using Yamaanco.Domain.Entities.ProfileEntities;
using Yamaanco.Domain.Enums;

namespace Yamaanco.Domain.Entities.BaseEntities
{
    public class Notification
    {
        public Notification()
        {
        }

        public Notification(string profileId, string title, string content, string participantId, string sourceId, NotificationType notificationType, NotificationCategory notificationCategory)
        {
            ProfileId = profileId;
            Id = Guid.NewGuid().ToString();
            Title = title;
            Content = content;
            CreatedDate = DateTime.Now;
            ParticipantId = participantId;
            IsSeen = false;
            SourceId = sourceId;
            NotificationType = notificationType;
            NotificationCategory = notificationCategory;
        }

        public void SetAsSeen()
        {
            IsSeen = true;
        }

        public string ProfileId { get; protected set; }
        public Profile Profile { get; protected set; }
        public string Id { get; protected set; }
        public string Title { get; protected set; }
        public string Content { get; protected set; }
        public DateTime CreatedDate { get; protected set; }
        public string ParticipantId { get; protected set; }
        public bool IsSeen { get; protected set; }
        public string SourceId { get; protected set; }
        public Profile Participant { get; protected set; }
        public NotificationType NotificationType { get; protected set; }
        public NotificationCategory NotificationCategory { get; protected set; }
    }
}