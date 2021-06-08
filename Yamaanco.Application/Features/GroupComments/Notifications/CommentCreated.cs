using MediatR;
using Yamaanco.Application.DTOs.Comment;

namespace Yamaanco.Application.Features.GroupComments.Notifications
{
    public class CommentCreated : INotification
    {
        public CommentDto Comment { get; set; }
    }
}