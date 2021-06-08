using MediatR;
using System.Collections.Generic;
using Yamaanco.Application.DTOs.Comment;

namespace Yamaanco.Application.Features.ProfileComments.Notifications
{
    public class CommentUpdated : INotification
    {
        public string Id { get; set; }
        public string Parent { get; set; }
        public string Root { get; set; }
        public string Content { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string UpdatedById { get; set; }
        public string UpdatedByName { get; set; }
        public string[] Pings { get; set; }
        public IList<CommentResourcesDto> Attachments { get; set; }
    }
}