using System;
using System.Collections.Generic;
using Yamaanco.Application.DTOs.Common;
using Yamaanco.Domain.Enums;

namespace Yamaanco.Application.DTOs.Comment
{
    public class CommentDto
    {
        public string Id { get; set; }
        public string Parent { get; set; }
        public string Root { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public string CreatedById { get; set; }
        public string CreatorName { get; set; }
        public string ProfilePictureUrl { get; set; }
        public int UpvoteCount { get; set; }
        public int ViewCount { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public CommentCategory Category { get; set; }
        public IEnumerable<CommentPingsDto> Pings { get; set; }
        public IEnumerable<ResourcesBasicInfoDto> Attachments { get; set; }

        public CommentDto()
        {
            Pings = new List<CommentPingsDto>();
            Attachments = new List<ResourcesBasicInfoDto>();
        }
    }
}