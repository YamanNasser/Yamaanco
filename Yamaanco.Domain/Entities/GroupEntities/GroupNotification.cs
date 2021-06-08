using Yamaanco.Domain.Entities.BaseEntities;
using Yamaanco.Domain.Enums;

namespace Yamaanco.Domain.Entities.GroupEntities
{
    public class GroupNotification : Notification
    {
        public GroupNotification()
        {
        }

        public GroupNotification(string groupId, string profileId, string title, string content, string participantId, string sourceId, NotificationType notificationType, NotificationCategory notificationCategory) : base(profileId, title, content, participantId, sourceId, notificationType, notificationCategory)
        {
            GroupId = groupId;
        }

        public string Target
        {
            get
            {
                return GroupId;
            }
            private set
            {
                _ = GroupId;
            }
        }

        public string GroupId { get; private set; }
        public Group Group { get; private set; }
        public GroupComment Comment { get; private set; }
    }
}