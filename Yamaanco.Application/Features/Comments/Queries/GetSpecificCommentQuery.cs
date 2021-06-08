using MediatR;
using System.Collections.Generic;
using Yamaanco.Application.ApiResponses;
using Yamaanco.Application.Common.Responses;
using Yamaanco.Application.DTOs.Comment;

namespace Yamaanco.Application.Features.Comments.Queries
{
    public class GetSpecificCommentQuery : IRequest<Response<IEnumerable<CommentDto>>>
    {
        public GetSpecificCommentQuery(string commentId)
        {
            CommentId = commentId;
        }

        public string CommentId { get; set; }
    }
}