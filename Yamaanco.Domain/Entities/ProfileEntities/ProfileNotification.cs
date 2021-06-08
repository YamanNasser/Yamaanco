using Yamaanco.Domain.Entities.BaseEntities;
using Yamaanco.Domain.Enums;
using Yamaanco.Domain.ValueObjects;

namespace Yamaanco.Domain.Entities.ProfileEntities
{
    public class ProfileNotification : Notification
    {
        public ProfileNotification()
        {
        }

        public ProfileNotification(string profileId, string title, string content, string participantId, string sourceId, NotificationType notificationType, NotificationCategory notificationCategory) : base(profileId, title, content, participantId, sourceId, notificationType, notificationCategory)
        {
        }

        public string Target
        {
            get => (new PairTarget(ProfileId, ParticipantId)).ToString();
            private set
            {
                _ = (new PairTarget(ProfileId, ParticipantId)).ToString();
            }
        }

        public ProfileComment Comment { get; private set; }
    }
}