using System;
using Yamaanco.Domain.Entities.BaseEntities;
using Yamaanco.Domain.Enums;

namespace Yamaanco.Domain.Entities.ProfileEntities
{
    public class ProfilePhotoResources : Resources
    {
        public ProfilePhotoResources()
        {
        }

        public ProfilePhotoResources(string folderName, PhotoSize photoSize, string profileId, string description) : base
                  ()
        {
            SetPath(folderName);
            ProfileId = profileId;
            PhotoSize = photoSize;
            Description = description;
        }

        public string FullPath
        {
            get => (new FullPath(Path, ProfileId, Extension)).ToString();
            private set => _ = (new FullPath(Path, ProfileId, Extension)).ToString();
        }

        public PhotoSize PhotoSize { get; private set; }
        public string ProfileId { get; private set; }
        public Profile Profile { get; private set; }

        public void SetPath(string folderName)
        {
            if (!string.IsNullOrEmpty(folderName))
                Path = $"{folderName}\\{PhotoSize}\\{ProfileId}";
        }
    }
}