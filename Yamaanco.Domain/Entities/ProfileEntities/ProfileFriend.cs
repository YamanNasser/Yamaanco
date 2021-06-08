using System;

namespace Yamaanco.Domain.Entities.ProfileEntities
{
    public class ProfileFriend
    {
        public ProfileFriend()
        {
        }

        public ProfileFriend(string friendProfileId, string profileId)
        {
            Id = Guid.NewGuid().ToString();
            FriendProfileId = friendProfileId;
            ProfileId = profileId;
        }

        public string Id { get; private set; }
        public string FriendProfileId { get; private set; }
        public string ProfileId { get; private set; }
        public Profile Profile { get; private set; }
        public Profile FriendProfile { get; private set; }
    }
}