using MediatR;
using System;
using System.Collections.Generic;
using Yamaanco.Application.Common.Responses;
using Yamaanco.Application.DTOs.Comment;


namespace Yamaanco.Application.Features.Comments.Queries
{
    public class GetCommentMentionsListQuery : IRequest<PagedResponse<IEnumerable<CommentMentionsInfoDto>>>
    {
        public int PageSize { get; set; } = 15;
        public int PageIndex { get; set; } = 0;
        public string UserName { get; set; }
        public string CommentId { get; set; }
    }
}
