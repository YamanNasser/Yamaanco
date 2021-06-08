using MediatR;
using System;
using System.Collections.Generic;
using Yamaanco.Application.Common.Responses;
using Yamaanco.Application.DTOs.Comment;

namespace Yamaanco.Application.Features.Comments.Queries
{
    public class FindCommentsByHashtagsQuery : IRequest<PagedResponse<IEnumerable<CommentDto>>>
    {
        public int PageSize { get; set; } = 15;
        public int PageIndex { get; set; } = 0;
        public string Hashtag { get; set; }
    }
}