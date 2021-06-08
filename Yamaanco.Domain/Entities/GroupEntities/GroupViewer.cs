using System;
using Yamaanco.Domain.Entities.ProfileEntities;

namespace Yamaanco.Domain.Entities.GroupEntities
{
    public class GroupViewer
    {
        public GroupViewer()
        {
        }

        public GroupViewer(string viewerProfileId, string groupId)
        {
            Id = Guid.NewGuid().ToString();
            ViewerProfileId = viewerProfileId;
            GroupId = groupId;
            ViewerDate = DateTime.Now;
        }

        public string Id { get; private set; }
        public string ViewerProfileId { get; private set; }
        public string GroupId { get; private set; }
        public Group Group { get; private set; }
        public Profile ViewerProfile { get; private set; }
        public DateTime ViewerDate { get; private set; }
    }
}