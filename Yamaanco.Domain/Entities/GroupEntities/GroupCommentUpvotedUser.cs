using System;
using Yamaanco.Domain.Entities.ProfileEntities;

namespace Yamaanco.Domain.Entities.GroupEntities
{
    public class GroupCommentUpvotedUser
    {
        public GroupCommentUpvotedUser()
        {
        }

        public GroupCommentUpvotedUser(string commentId, string groupId, string profileId)
        {
            Id = Guid.NewGuid().ToString();
            CommentId = commentId;
            Date = DateTime.Now;
            GroupId = groupId;
            ProfileId = profileId;
        }

        public string Id { get; private set; }
        public string CommentId { get; private set; }
        public DateTime Date { get; private set; }
        public string GroupId { get; private set; }
        public string ProfileId { get; private set; }
        public Profile Profile { get; private set; }
        public GroupComment Comment { get; private set; }
        public Group Group { get; private set; }
    }
}