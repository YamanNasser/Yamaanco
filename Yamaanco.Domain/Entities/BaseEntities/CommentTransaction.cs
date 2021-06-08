using System;
using Yamaanco.Domain.Enums;

namespace Yamaanco.Domain.Entities.BaseEntities
{
    public class CommentTransaction
    {
        public CommentTransaction()
        {
        }

        protected CommentTransaction(string commentId, string commentRoot, string commentParent, string userId, CommentTransactionType commentTransactionType, string data)
        {
            Id = Guid.NewGuid().ToString();
            CommentId = commentId;
            CommentRoot = commentRoot;
            CommentParent = commentParent;
            UserId = userId;
            CommentTransactionType = commentTransactionType;
            CreatedDate = DateTime.Now;
            Data = data;
        }

        public string Id { get; protected set; }
        public string CommentId { get; protected set; }
        public string CommentRoot { get; protected set; }
        public string CommentParent { get; protected set; }
        public string UserId { get; protected set; }
        public CommentTransactionType CommentTransactionType { get; protected set; }
        public DateTime CreatedDate { get; protected set; }
        public string Data { get; protected set; }
    }
}