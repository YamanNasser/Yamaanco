using System;

namespace Yamaanco.Domain.Entities.ProfileEntities
{
    public class ProfileCommentUpvotedUser
    {
        public ProfileCommentUpvotedUser()
        {
        }

        public ProfileCommentUpvotedUser(string commentId, string profileId)
        {
            Id = Guid.NewGuid().ToString();
            CommentId = commentId;
            Date = DateTime.Now;
            ProfileId = profileId;
        }

        public string Id { get; private set; }
        public string CommentId { get; private set; }
        public DateTime Date { get; private set; }
        public string ProfileId { get; private set; }
        public ProfileComment Comment { get; private set; }
        public Profile Profile { get; private set; }
    }
}