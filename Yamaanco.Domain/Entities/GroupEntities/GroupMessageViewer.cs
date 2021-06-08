using System;
using Yamaanco.Domain.Entities.ProfileEntities;

namespace Yamaanco.Domain.Entities.GroupEntities
{
    public class GroupMessageViewer
    {
        public GroupMessageViewer()
        {
        }

        public GroupMessageViewer(string messageId, string groupId, string profileId)
        {
            Id = Guid.NewGuid().ToString();
            MessageId = messageId;
            Date = DateTime.Now;
            GroupId = groupId;
            ProfileId = profileId;
        }

        public string Id { get; private set; }
        public string MessageId { get; private set; }
        public DateTime Date { get; private set; }
        public string GroupId { get; private set; }
        public string ProfileId { get; private set; }
        public Profile Profile { get; private set; }
        public GroupMessage Message { get; private set; }
        public Group Group { get; private set; }
    }
}