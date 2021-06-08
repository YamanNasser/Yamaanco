using System;

namespace Yamaanco.Domain.Entities.ProfileEntities
{
    public class ProfileFollower
    {
        public ProfileFollower()
        {
        }

        public ProfileFollower(string profileId, string followerProfileId)
        {
            Id = Guid.NewGuid().ToString();
            ProfileId = profileId;
            FollowerProfileId = followerProfileId;
            IsSeen = false;
        }

        public string Id { get; private set; }
        public string ProfileId { get; private set; }
        public string FollowerProfileId { get; private set; }
        public DateTime FollowedDate { get; private set; }
        public bool IsSeen { get; private set; }
        public DateTime SeenDate { get; private set; }
        public Profile Profile { get; private set; }
        public Profile FollowerProfile { get; private set; }
    }
}