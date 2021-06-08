using Yamaanco.Application.Common.Mappings;
using Yamaanco.Domain.Entities.ProfileEntities;

namespace Yamaanco.Application.DTOs.Comment
{
    public class CommentResourcesDto
    {
        public string Id { get; set; }
        public string Path { get; set; }
        public string ContentType { get; set; }
        public long Length { get; set; }
        public string Description { get; set; }
        public string CommentId { get; set; }
        public string CategoryId { get; set; }
    }
}