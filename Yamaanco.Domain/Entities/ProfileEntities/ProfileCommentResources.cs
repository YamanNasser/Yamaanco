using System;
using Yamaanco.Domain.Entities.BaseEntities;

namespace Yamaanco.Domain.Entities.ProfileEntities
{
    public class ProfileCommentResources : Resources
    {
        public ProfileCommentResources()
        {
        }

        public ProfileCommentResources(string folderName, string profileId, string commentId, string description) : base
            ()
        {
            if (!string.IsNullOrEmpty(folderName))
                Path = $"{folderName}\\{profileId}\\{commentId}";
            CommentId = commentId;
            ProfileId = profileId;
            Description = description;
        }

        public string FullPath
        {
            get => (new FullPath(Path, ProfileId, Extension)).ToString();
            private set
            {
                _ = (new FullPath(Path, ProfileId, Extension)).ToString();
            }
        }

        public string CommentId { get; private set; }
        public string ProfileId { get; private set; }

        public ProfileComment Comment { get; private set; }
        public Profile Profile { get; private set; }
    }
}