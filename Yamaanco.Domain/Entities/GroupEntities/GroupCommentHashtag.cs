using Yamaanco.Domain.Entities.BaseEntities;
using Yamaanco.Domain.Entities.ProfileEntities;

namespace Yamaanco.Domain.Entities.GroupEntities
{
    public class GroupCommentHashtag : CommentHashtag
    {
        public GroupCommentHashtag()
        {
        }

        public GroupCommentHashtag(string groupId, string commentId, string hashtag) : base(commentId, hashtag)
        {
            GroupId = groupId;
        }

        public string GroupId { get; private set; }
        public GroupComment Comment { get; private set; }
        public Group Group { get; private set; }
    }
}