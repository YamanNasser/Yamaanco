using MediatR;

namespace Yamaanco.Application.Features.ProfileComments.Notifications
{
    public class CommentUpvoted : INotification
    {
        public string Id { get; set; }
        public string Parent { get; set; }
        public string Root { get; set; }
        public string Content { get; set; }
        public string ProfileId { get; set; }
        public string ProfileName { get; set; }
        public string UpvoteById { get; set; }
        public string UpvoteByName { get; set; }
        public string CreatedById { get; set; }
    }
}