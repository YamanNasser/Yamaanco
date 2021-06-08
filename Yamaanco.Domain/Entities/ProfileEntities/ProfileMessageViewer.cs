using System;

namespace Yamaanco.Domain.Entities.ProfileEntities
{
    public class ProfileMessageViewer
    {
        public ProfileMessageViewer()
        {
        }

        public ProfileMessageViewer(string messageId, string profileId)
        {
            Id = Guid.NewGuid().ToString();
            MessageId = messageId;
            ProfileId = profileId;
            Date = DateTime.Now;
        }

        public string Id { get; private set; }
        public string MessageId { get; private set; }
        public string ProfileId { get; private set; }
        public DateTime Date { get; private set; }
        public ProfileMessage Message { get; private set; }
        public Profile Profile { get; private set; }
    }
}