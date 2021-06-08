using Yamaanco.Domain.Entities.BaseEntities;
using Yamaanco.Domain.Enums;

namespace Yamaanco.Domain.Entities.GroupEntities
{
    public class GroupCommentTransaction : CommentTransaction
    {
        public GroupCommentTransaction()
        {
        }

        public GroupCommentTransaction(string groupId, string commentId, string commentRoot, string commentParent, string userId, CommentTransactionType commentTransactionType, string data) : base(commentId, commentRoot, commentParent, userId, commentTransactionType, data)
        {
            GroupId = groupId;
        }

        public string GroupId { get; private set; }
        public Group Group { get; private set; }
        public GroupComment Comment { get; private set; }
    }
}