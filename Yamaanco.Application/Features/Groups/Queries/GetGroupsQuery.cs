using MediatR;
using System.Collections.Generic;
using Yamaanco.Application.Common.Responses;
using Yamaanco.Application.DTOs.Group;

namespace Yamaanco.Application.Features.Groups.Queries
{
    public class GetGroupsQuery : IRequest<PagedResponse<IEnumerable<GroupFilterResultDto>>
>
    {
        public string Filter { get; set; }
        public string SortColumn { get; set; }
        public string SortColumnDirection { get; set; }
        public int PageSize { get; set; } = 5;
        public int PageIndex { get; set; } = 0;
    }
}