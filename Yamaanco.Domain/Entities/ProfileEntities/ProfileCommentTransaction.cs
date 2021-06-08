using Yamaanco.Domain.Entities.BaseEntities;
using Yamaanco.Domain.Enums;

namespace Yamaanco.Domain.Entities.ProfileEntities
{
    public class ProfileCommentTransaction : CommentTransaction
    {
        public ProfileCommentTransaction()
        {
        }

        public ProfileCommentTransaction(string profileId, string commentId, string commentRoot, string commentParent, string userId, CommentTransactionType commentTransactionType, string data) : base(commentId, commentRoot, commentParent, userId, commentTransactionType, data)
        {
            ProfileId = profileId;
        }

        public string ProfileId { get; private set; }
        public Profile Profile { get; private set; }
    }
}