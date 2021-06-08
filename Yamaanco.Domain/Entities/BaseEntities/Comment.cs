using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Yamaanco.Domain.Common;
using Yamaanco.Domain.Enums;

namespace Yamaanco.Domain.Entities.BaseEntities
{
    public class Comment : AuditableEntity
    {
        public Comment()
        {
        }

        protected Comment(string parent, string root, string content, CommentCategory category, CommentType type, CommentClassification classification, string createdById) : base(createdById)
        {
            Id = Guid.NewGuid().ToString();
            Parent = parent;
            Root = root;
            Content = content;
            Category = category;
            Type = type;
            Classification = classification;
        }

        protected IEnumerator<string> GenerateHashtags()
        {
            foreach (Match hash in Regex.Matches(Content, @"\s([#][\w_-]+)"))
                yield return hash.Value;
        }

        public virtual int Upvote()
        {
            return UpvoteCount += 1;
        }

        public virtual int CancelVoting()
        {
            return UpvoteCount -= 1;
        }

        public virtual int NewViewer()
        {
            return UpvoteCount += 1;
        }

        public string Id { get; protected set; }
        public string Parent { get; protected set; }
        public string Root { get; protected set; }
        public string Content { get; protected set; }
        public int UpvoteCount { get; protected set; }
        public int ViewCount { get; protected set; }
        public CommentCategory Category { get; protected set; }
        public CommentType Type { get; protected set; }
        public CommentClassification Classification { get; protected set; }
    }
}