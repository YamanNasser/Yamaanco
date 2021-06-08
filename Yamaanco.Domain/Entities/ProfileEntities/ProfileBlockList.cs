using System;

namespace Yamaanco.Domain.Entities.ProfileEntities
{
    public class ProfileBlockList
    {
        public ProfileBlockList()
        {
        }

        public ProfileBlockList(string profileId, string blockProfileId, DateTime createdDate, string reason)
        {
            Id = Guid.NewGuid().ToString();
            ProfileId = profileId;
            BlockProfileId = blockProfileId;
            CreatedDate = createdDate;
            Reason = reason;
        }

        public string Id { get; private set; }
        public string ProfileId { get; private set; }
        public string BlockProfileId { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public string Reason { get; private set; }
        public Profile Profile { get; private set; }
        public Profile BlockProfile { get; private set; }
    }
}