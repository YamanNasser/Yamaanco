using MediatR;
using System.Collections.Generic;
using Yamaanco.Application.Common.Responses;
using Yamaanco.Application.DTOs.Comment;

namespace Yamaanco.Application.Features.ProfileComments.Queries
{
    public class GetCommentsQuery : IRequest<PagedResponse<IEnumerable<CommentDto>>>
    {
        public string ProfileId { get; set; }
        public int PageSize { get; set; } = 15;
        public int PageIndex { get; set; } = 0;
    }
}