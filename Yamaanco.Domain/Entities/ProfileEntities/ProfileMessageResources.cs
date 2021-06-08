using Yamaanco.Domain.Entities.BaseEntities;

namespace Yamaanco.Domain.Entities.ProfileEntities
{
    public class ProfileMessageResources : Resources
    {
        public ProfileMessageResources()
        {
        }

        public ProfileMessageResources(string folderName, string profileId, string messageId, string description) : base
               ()
        {
            if (!string.IsNullOrEmpty(folderName))
                Path = $"{folderName}\\{profileId}\\{messageId}";
            MessageId = messageId;
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

        public string MessageId { get; private set; }
        public string ProfileId { get; private set; }

        public ProfileMessage Message { get; private set; }
        public Profile Profile { get; private set; }
    }
}