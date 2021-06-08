using Yamaanco.Domain.Entities.BaseEntities;
using Yamaanco.Domain.Enums;
using Yamaanco.Domain.ValueObjects;

namespace Yamaanco.Domain.Entities.ProfileEntities
{
    public class ProfileMessage : Message
    {
        public ProfileMessage()
        {
        }

        public ProfileMessage(string profileId, string content, MessageCategory messageType, string createdById) : base(content, messageType, createdById)
        {
            ProfileId = profileId;
        }

        public void AttachFile(ProfileMessageResources file)
        {
            FileId = file.Id;
            File = file;
        }

        public string ProfileId { get; private set; }
        public Profile Profile { get; private set; }
        public Profile CreatedBy { get; private set; }
        public ProfileMessageResources File { get; private set; }

        public string Target
        {
            get => (new PairTarget(ProfileId, CreatedById)).ToString();
            private set
            {
                _ = (new PairTarget(ProfileId, CreatedById)).ToString();
            }
        }
    }
}