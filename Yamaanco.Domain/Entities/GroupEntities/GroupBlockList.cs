using System;
using Yamaanco.Domain.Entities.ProfileEntities;

namespace Yamaanco.Domain.Entities.GroupEntities
{
    public class GroupBlockList
    {
        public GroupBlockList()
        {
        }

        public GroupBlockList(string groupId, string blockProfileId, string reason)
        {
            Id = Guid.NewGuid().ToString();
            GroupId = groupId;
            BlockProfileId = blockProfileId;
            CreatedDate = DateTime.Now;
            Reason = reason;
        }

        public string Id { get; private set; }
        public string GroupId { get; private set; }
        public string BlockProfileId { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public string Reason { get; private set; }
        public Group Group { get; private set; }
        public Profile BlockProfile { get; private set; }
    }
}