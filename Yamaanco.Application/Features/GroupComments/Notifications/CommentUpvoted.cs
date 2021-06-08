using MediatR;

namespace Yamaanco.Application.Features.GroupComments.Notifications
{
    public class CommentUpvoted : INotification
    {
        public string Id { get; set; }
        public string Parent { get; set; }
        public string Root { get; set; }
        public string Content { get; set; }
        public string GroupId { get; set; }
        public string GroupName { get; set; }
        public string UpvoteById { get; set; }
        public string CreatedById { get; set; }

        public string UpvoteByName { get; set; }
    }
}