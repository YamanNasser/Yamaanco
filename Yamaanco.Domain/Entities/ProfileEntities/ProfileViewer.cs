using System;

namespace Yamaanco.Domain.Entities.ProfileEntities
{
    public class ProfileViewer
    {
        public ProfileViewer()
        {
        }

        public ProfileViewer(string viewerProfileId, string profileId)
        {
            Id = Guid.NewGuid().ToString();
            ViewerProfileId = viewerProfileId;
            ProfileId = profileId;
            ViewerDate = DateTime.Now;
        }

        public string Id { get; private set; }
        public string ViewerProfileId { get; private set; }
        public string ProfileId { get; private set; }
        public Profile Profile { get; private set; }
        public Profile ViewerProfile { get; private set; }
        public DateTime ViewerDate { get; private set; }
    }
}