using System;
using Yamaanco.Domain.Entities.ProfileEntities;

namespace Yamaanco.Domain.Entities.BaseEntities
{
    public class CommentPings
    {
        protected CommentPings()
        {
        }

        protected CommentPings(string commentId, string mentionedUserId)
        {
            Id = Guid.NewGuid().ToString();
            CommentId = commentId;
            MentionedUserId = mentionedUserId;
        }

        public string Id { get; protected set; }
        public string CommentId { get; protected set; }
        public string MentionedUserId { get; protected set; }
        public Profile MentionedUser { get; protected set; }
    }
}