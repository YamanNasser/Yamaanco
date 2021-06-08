using System;

namespace Yamaanco.Domain.Common
{
    public class AuditableEntity
    {
        public AuditableEntity()
        {
        }

        protected AuditableEntity(string createdById)
        {
            CreatedById = createdById;
            Created = DateTime.Now;
            LastModifiedById = createdById;
            LastModified = DateTime.Now;
            IsDeleted = false;
        }

        public virtual void Delete()
        {
            IsDeleted = true;
        }

        public string CreatedById { get; protected set; }

        public DateTime Created { get; protected set; }

        public string LastModifiedById { get; protected set; }

        public DateTime? LastModified { get; protected set; }

        public bool IsDeleted { get; protected set; }
    }
}