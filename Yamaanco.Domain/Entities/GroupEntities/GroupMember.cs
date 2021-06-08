using System;
using Yamaanco.Domain.Entities.ProfileEntities;

namespace Yamaanco.Domain.Entities.GroupEntities
{
    public class GroupMember
    {
        public GroupMember()
        {
        }

        public GroupMember(string groupId, string memberId, bool isAdmin)
        {
            Id = Guid.NewGuid().ToString();
            GroupId = groupId;
            MemberId = memberId;
            IsAdmin = isAdmin;
            JoinDate = DateTime.Now;
        }

        public string Id { get; private set; }
        public string GroupId { get; private set; }
        public string MemberId { get; private set; }
        public bool IsAdmin { get; private set; }
        public DateTime JoinDate { get; private set; }
        public Profile Member { get; private set; }
        public Group Group { get; private set; }
    }
}