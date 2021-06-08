using Yamaanco.Domain.Entities.BaseEntities;

namespace Yamaanco.Domain.Entities.ProfileEntities
{
    public class ProfileCommentHashtag : CommentHashtag
    {
        public ProfileCommentHashtag()
        {
        }

        public ProfileCommentHashtag(string profileId, string commentId, string hashtag) : base(commentId, hashtag)
        {
            ProfileId = profileId;
        }

        public string ProfileId { get; private set; }
        public ProfileComment Comment { get; private set; }
        public Profile Profile { get; private set; }
    }
}