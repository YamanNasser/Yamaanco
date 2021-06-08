using Yamaanco.Domain.Entities.BaseEntities;

namespace Yamaanco.Domain.Entities.ProfileEntities
{
    public class ProfileCommentPings : CommentPings
    {
        public ProfileCommentPings()
        {
        }

        public ProfileCommentPings(string profileId, string commentId, string mentionedUserId) : base(commentId, mentionedUserId)
        {
            ProfileId = profileId;
        }

        public string ProfileId { get; private set; }
        public ProfileComment Comment { get; private set; }
        public Profile Profile { get; private set; }
    }
}