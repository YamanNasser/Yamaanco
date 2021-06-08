using MediatR;
using Yamaanco.Application.ApiResponses;

namespace Yamaanco.Application.Features.ProfileComments.Commands
{
    public class UpdateCommentUpvoteCommand : IRequest<Response<int>>
    {
        public UpdateCommentUpvoteCommand(string commentId)
        {
            CommentId = commentId;
        }

        public string CommentId { get; set; }
    }
}