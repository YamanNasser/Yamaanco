using MediatR;
using System.Collections.Generic;
using Yamaanco.Application.DTOs.Comment;

namespace Yamaanco.Application.Features.ProfileComments.Notifications
{
    public class CommentsReceived : INotification
    {
        public IList<CommentDto> ReceivedResult { get; set; }
        public string ViewerId { get; set; }
    }
}