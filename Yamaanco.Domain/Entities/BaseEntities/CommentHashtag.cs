using System;
using System.Collections.Generic;
using System.Text;

namespace Yamaanco.Domain.Entities.BaseEntities
{
    public class CommentHashtag
    {
        public CommentHashtag()
        {
        }

        protected CommentHashtag(string commentId, string hashtag)
        {
            Id = Guid.NewGuid().ToString();
            CommentId = commentId;
            Hashtag = hashtag;
        }

        public string Id { get; protected set; }
        public string CommentId { get; protected set; }
        public string Hashtag { get; protected set; }
    }
}