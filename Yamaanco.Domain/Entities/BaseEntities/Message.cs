using System;
using Yamaanco.Domain.Common;
using Yamaanco.Domain.Enums;

namespace Yamaanco.Domain.Entities.BaseEntities
{
    public class Message : AuditableEntity
    {
        public Message()
        {
        }

        protected Message(string content, MessageCategory messageType, string createdById) : base(createdById)
        {
            Id = Guid.NewGuid().ToString();
            Content = content;
            MessageType = messageType;
            MessageType = messageType;
            ViewCount = 0;
        }

        protected void Update(string content, string fileId, string lastModifiedById)
        {
            Content = content;
            FileId = fileId;
            LastModifiedById = lastModifiedById;
        }

        public virtual int AddNewViewer()
        {
            return ViewCount += 1;
        }

        public string Id { get; protected set; }
        public string Content { get; protected set; }
        public int ViewCount { get; protected set; }
        public MessageCategory MessageType { get; protected set; }
        public string FileId { get; protected set; }
    }
}