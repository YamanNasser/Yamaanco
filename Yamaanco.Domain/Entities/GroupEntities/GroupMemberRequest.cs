using System;
using Yamaanco.Domain.Entities.ProfileEntities;
using Yamaanco.Domain.ValueObjects;

namespace Yamaanco.Domain.Entities.GroupEntities
{
    public class GroupMemberRequest
    {
        public GroupMemberRequest()
        {
        }

        public GroupMemberRequest(string groupId, string inviterId, string invitedEmail)
        {
            Id = Guid.NewGuid().ToString();
            GroupId = groupId;
            InviterId = inviterId;
            InvitedEmail = invitedEmail;
            RequestDate = DateTime.Now;
        }

        public string Id { get; private set; }

        public string GroupId { get; private set; }

        public string InviterId { get; private set; }

        public string InvitedEmail { get; private set; }

        public string Code
        {
            get
            {
                return (new MemberRequestCode(Id, GroupId, InviterId, InvitedEmail, RequestDate)).ToHashString();
            }
            private set
            {
                _ = (new MemberRequestCode(Id, GroupId, InviterId, InvitedEmail, RequestDate))
                    .ToHashString();
            }
        }

        public DateTime RequestDate { get; private set; }

        public Profile Inviter { get; private set; }
        public Group Group { get; private set; }
    }
}