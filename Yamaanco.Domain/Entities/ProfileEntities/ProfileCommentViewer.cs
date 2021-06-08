using System;

namespace Yamaanco.Domain.Entities.ProfileEntities
{
    public class ProfileCommentViewer
    {
        public ProfileCommentViewer()
        {
        }

        public ProfileCommentViewer(string commentId, string viewerProfileId, string profileId)
        {
            Id = Guid.NewGuid().ToString();
            CommentId = commentId;
            ViewerProfileId = viewerProfileId;
            ProfileId = profileId;
            Date = DateTime.Now;
        }

        public string Id { get; private set; }
        public string CommentId { get; private set; }
        public string ViewerProfileId { get; private set; }
        public string ProfileId { get; private set; }
        public DateTime Date { get; private set; }
        public ProfileComment Comment { get; private set; }
        public Profile ViewerProfile { get; private set; }
        public Profile Profile { get; private set; }
    }
}