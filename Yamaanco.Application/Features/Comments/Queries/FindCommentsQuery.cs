using MediatR;
using System;
using System.Collections.Generic;
using Yamaanco.Application.Common.Responses;
using Yamaanco.Application.DTOs.Comment;

namespace Yamaanco.Application.Features.Comments.Queries
{
    public class FindCommentsQuery : IRequest<PagedResponse<IEnumerable<CommentDto>>>
    {
        public int PageSize { get; set; } = 15;
        public int PageIndex { get; set; } = 0;
        public string Contains { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string CreatorName { get; set; }
        public string CategoryName { get; set; }
    }
}