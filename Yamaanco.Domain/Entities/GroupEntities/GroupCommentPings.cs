using Yamaanco.Domain.Entities.BaseEntities;

namespace Yamaanco.Domain.Entities.GroupEntities
{
    public class GroupCommentPings : CommentPings
    {
        public GroupCommentPings()
        {
        }

        public GroupCommentPings(string groupId, string commentId, string mentionedUserId) : base(commentId, mentionedUserId)
        {
            GroupId = groupId;
        }

        public string GroupId { get; private set; }
        public GroupComment Comment { get; private set; }
        public Group Group { get; private set; }
    }
}