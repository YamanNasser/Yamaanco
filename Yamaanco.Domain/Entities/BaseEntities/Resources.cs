using System;
using Yamaanco.Domain.Enums;

namespace Yamaanco.Domain.Entities.BaseEntities
{
    public class Resources
    {
        protected Resources()
        {
            Id = Guid.NewGuid().ToString();
        }

        public void SetProperties(int length, string contentType, string extension, string description = null)
        {
            Length = length;
            ContentType = contentType;
            Extension = extension;

            if (description != null)
                Description = description;
        }

        public string Id { get; protected set; }
        public string Extension { get; protected set; }
        public string Path { get; protected set; }
        public string ContentType { get; protected set; }
        public long Length { get; protected set; }
        public string Description { get; protected set; }
    }
}