using MediatR;
using Yamaanco.Application.DTOs.Comment;

namespace Yamaanco.Application.Features.ProfileComments.Notifications
{
    public class CommentCreated : INotification
    {
        public CommentDto Comment { get; set; }
    }
}